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

        public Main_Form()
        {
            InitializeComponent();
            picture = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(picture);
            spaceObjects = new List<SpaceObject>();
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
                    spaceObjects[i].Position += spaceObjects[i].Velocity;
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (pattern_comboBox.SelectedIndex == -1)
                return;
            var property = typeof(SpaceObject).GetProperty(pattern_comboBox.SelectedItem.ToString());
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
                velocity_label.Text = "Velocity: " + (mouse.Position - mouse.SpaceObject.Position).Length.ToString();
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
            if (!tail_checkBox.Checked)
                graphics.Clear(Color.Black);
            for (int i = 0; i < spaceObjects.Count; i++)
            {
                float radius = spaceObjects[i].Radius * 0.5f;
                float x = spaceObjects[i].Position.X - radius * 0.5f;
                float y = spaceObjects[i].Position.Y - radius * 0.5f;
                graphics.DrawEllipse(new Pen(spaceObjects[i].Color, radius), x, y, radius, radius);
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
                graphics.ScaleTransform(1.099f, 1.099f);
                scale++;
            }
            if (e.KeyCode == Keys.Q && scale > -40)
            {
                graphics.ScaleTransform(0.9099f, 0.9099f);
                scale--;
            }
            if (e.KeyCode == Keys.D)
            {
                graphics.TranslateTransform(-_Scale * 20f, 0f);
                offset.X += 20f;
            }
            if (e.KeyCode == Keys.A)
            {
                graphics.TranslateTransform(_Scale * 20f, 0f);
                offset.X -= 20f;
            }
            if (e.KeyCode == Keys.S)
            {
                graphics.TranslateTransform(0f, -_Scale * 20f);
                offset.Y += 20f;
            }
            if (e.KeyCode == Keys.W)
            {
                graphics.TranslateTransform(0f, _Scale * 20f);
                offset.Y -= 20f;
            }
        }

        private void Pattern_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
