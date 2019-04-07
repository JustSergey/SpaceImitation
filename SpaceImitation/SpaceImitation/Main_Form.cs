using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceImitation
{
    public struct Vector
    {
        public float Length => (float)Math.Sqrt(X * X + Y * Y);
        public float X, Y;

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector(Vector vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public void Normalize()
        {
            this /= Length;
        }

        public void SetLength(float length)
        {
            Normalize();
            this *= length;
        }

        public float GetDistanceTo(Vector vector) => (vector - this).Length;

        public static Vector operator /(Vector v, float d) => new Vector(v.X / d, v.Y / d);
        public static Vector operator *(Vector v, float d) => new Vector(v.X * d, v.Y * d);
        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X , v1.Y + v2.Y);
        public static Vector operator -(Vector v1, Vector v2) => new Vector(v1.X - v2.X, v1.Y - v2.Y);
        public static implicit operator Vector(Point point) => new Vector(point.X, point.Y);
        public static implicit operator PointF(Vector vector) => new PointF(vector.X, vector.Y);
    }

    public struct Mouse
    {
        public bool IsDown;
        public Vector Position;
        public SpaceObject SpaceObject;
    }

    public partial class Main_Form : Form
    {
        private static Bitmap picture;
        private static Graphics graphics;
        private static List<SpaceObject> spaceObjects;
        private static Mouse mouse;
        private static int scale = 0;
        private float _Scale => (float)Math.Pow(0.9099, scale);
        private Vector offset;
        private Dictionary<Keys, Vector> direction = new Dictionary<Keys, Vector>()
        {
            { Keys.W, new Vector(0f, -20f) },
            { Keys.A, new Vector(-20f, 0f) },
            { Keys.S, new Vector(0f, 20f) },
            { Keys.D, new Vector(20f, 0f) }
        };

        public Main_Form()
        {
            InitializeComponent();
            picture = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(picture);
            spaceObjects = new List<SpaceObject>();

            var properties = typeof(SpaceObject).GetProperties();
            for (int i = 0; i < properties.Length; i++)
                pattern_comboBox.Items.Add(properties[i].Name);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < spaceObjects.Count; i++)
            {
                for (int j = 0; j < spaceObjects.Count && i < spaceObjects.Count; j++)
                {
                    if (i == j)
                        continue;

                    if ((spaceObjects[j].Position - spaceObjects[i].Position).Length <= (spaceObjects[j].Radius + spaceObjects[i].Radius) / 2)
                    {
                        if (spaceObjects[i].Radius > spaceObjects[j].Radius)
                            spaceObjects.Remove(spaceObjects[j]);
                        else
                            spaceObjects.Remove(spaceObjects[i]);
                        continue;
                    }

                    Vector direction = spaceObjects[j].Position - spaceObjects[i].Position;
                    float dist = direction.Length;
                    direction.SetLength(spaceObjects[j].Mass / (dist * dist));
                    spaceObjects[i].Velocity += direction;
                }
                if (i < spaceObjects.Count)
                {
                    spaceObjects[i].Position += spaceObjects[i].Velocity;

                    spaceObjects[i].Tail.Add(spaceObjects[i].Position);
                    while (spaceObjects[i].Tail.Count > tail_trackBar.Value)
                        spaceObjects[i].Tail.RemoveAt(0);
                }
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (pattern_comboBox.SelectedIndex == -1)
                return;
            var property = typeof(SpaceObject).GetProperties()[pattern_comboBox.SelectedIndex];
            SpaceObject spaceObject = (SpaceObject)property.GetValue(null);

            spaceObject.Position = e.Location;
            spaceObject.Position += offset;
            spaceObject.Position *= _Scale;
            spaceObjects.Add(spaceObject);
            mouse.SpaceObject = spaceObject;
            mouse.IsDown = true;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.Position = e.Location;
            mouse.Position += offset;
            mouse.Position *= _Scale;
            if (mouse.IsDown)
            {
                string len = (mouse.Position - mouse.SpaceObject.Position).Length.ToString();
                int index = len.IndexOf(',');
                if (len.Length > index + 3 && index != -1)
                    len = len.Substring(0, index + 3);
                velocity_label.Text = "Velocity: " + len;
            }
            else
                velocity_label.Text = "Velocity: 0.00";
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouse.IsDown)
            {
                mouse.IsDown = false;
                mouse.SpaceObject.Velocity = (mouse.Position - mouse.SpaceObject.Position) / 40f;
            }
        }

        private void Button_play_Click(object sender, EventArgs e)
        {
            if (button_play.Text == "Play")
            {
                button_play.Text = "Pause";
                timer.Enabled = true;
            }
            else
            {
                button_play.Text = "Play";
                timer.Enabled = false;
            }
        }

        private void Draw_timer_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);
            for (int i = 0; i < spaceObjects.Count; i++)
            {
                float radius = spaceObjects[i].Radius * 0.5f;
                float x = spaceObjects[i].Position.X - radius * 0.5f;
                float y = spaceObjects[i].Position.Y - radius * 0.5f;
                Pen pen = new Pen(spaceObjects[i].Color, radius);
                graphics.DrawEllipse(pen, x, y, radius, radius);
                for (int j = 1; j < spaceObjects[i].Tail.Count; j++)
                    graphics.DrawLine(pen, spaceObjects[i].Tail[j - 1], spaceObjects[i].Tail[j]);
            }
            if (mouse.IsDown) 
                graphics.DrawLine(new Pen(Color.Red, _Scale * 4.0f), mouse.SpaceObject.Position, mouse.Position);
            pictureBox.Image = picture;
        }

        private void Button_remove_Click(object sender, EventArgs e)
        {
            if (spaceObjects.Count > 0)
                spaceObjects.RemoveAt(spaceObjects.Count - 1);
        }

        private void Main_Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E && scale < 40)
            {
                graphics.TranslateTransform(mouse.Position.X, mouse.Position.Y);
                graphics.ScaleTransform(1.099f, 1.099f);
                graphics.TranslateTransform(-mouse.Position.X, -mouse.Position.Y);
                scale++;
                offset += mouse.Position * 0.0901f / _Scale;
            }
            else if (e.KeyCode == Keys.Q && scale > -40)
            {
                graphics.TranslateTransform(mouse.Position.X, mouse.Position.Y);
                graphics.ScaleTransform(0.9099f, 0.9099f);
                graphics.TranslateTransform(-mouse.Position.X, -mouse.Position.Y);
                scale--;
                offset -= mouse.Position * 0.099f / _Scale;
            }
            direction.TryGetValue(e.KeyCode, out Vector dir);
            offset += dir;
            dir *= -_Scale;
            graphics.TranslateTransform(dir.X, dir.Y);
        }

        private void Pattern_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Tail_trackBar_Scroll(object sender, EventArgs e)
        {
            tail_label.Text = tail_trackBar.Value.ToString();
        }

        private void Game_speed_trackBar_Scroll(object sender, EventArgs e)
        {
            timer.Interval = game_speed_trackBar.Value * game_speed_trackBar.Value;
            string game_speed_text = (3.0f / game_speed_trackBar.Value).ToString();
            if (game_speed_text.Length > 4)
                game_speed_text = game_speed_text.Substring(0, 4);
            game_speed_label.Text = game_speed_text;
        }
    }
}
