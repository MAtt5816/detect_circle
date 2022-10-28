using System.Drawing;

string directory = AppDomain.CurrentDomain.BaseDirectory;
Image image = Image.FromFile(directory + @"../../../in/in.png");

Bitmap bmp = new Bitmap(image);

int x = bmp.Width / 2;
int y = bmp.Height / 2;
int ringCount = 0;
int radius = 0;
int radius2 = 0;
int radius3 = 0;
int radius4 = 0;

do
{
    //  X axis
    //  find 2nd ring
    for (int i = x; i < bmp.Width; i++)
    {
        Color c1 = bmp.GetPixel(i, y);
        Color c2 = bmp.GetPixel(i - 10, y);
        if ((c1.R - c2.R > 130) && (c1.B - c2.B > 130) && (c1.G - c2.G > 130))
        {
            ringCount++;
            if (ringCount == 2)
            {
                radius = i - x;
                break;
            }
            i += 10;
        }
    }

    //  check if image is centered
    ringCount = 0;
    for (int i = x; i >= 0; i--)
    {
        Color c1 = bmp.GetPixel(i, y);
        Color c2 = bmp.GetPixel(i + 10, y);
        if ((c1.R - c2.R > 130) && (c1.B - c2.B > 130) && (c1.G - c2.G > 130))
        {
            ringCount++;
            if (ringCount == 2)
            {
                radius2 = x - i;
                break;
            }
            i -= 10;
        }
    }

    int centerShiftX = radius - radius2;
    if (centerShiftX > 0)
    {
        x += (centerShiftX/2);
        radius -= (centerShiftX / 2);
    }
    else if (centerShiftX < 0)
    {
        x -= (centerShiftX/2);
        radius += (centerShiftX / 2);
    }

    // Y axis
    //  find 2nd ring
    ringCount = 0;
    for (int i = y; i < bmp.Height; i++)
    {
        Color c1 = bmp.GetPixel(x, i);
        Color c2 = bmp.GetPixel(x, i - 10);
        if ((c1.R - c2.R > 130) && (c1.B - c2.B > 130) && (c1.G - c2.G > 130))
        {
            ringCount++;
            if (ringCount == 2)
            {
                radius3 = i - y;
                break;
            }
            i += 10;
        }
    }

    //  check if image is centered
    ringCount = 0;
    for (int i = y; i >= 0; i--)
    {
        Color c1 = bmp.GetPixel(x, i);
        Color c2 = bmp.GetPixel(x, i + 10);
        if ((c1.R - c2.R > 130) && (c1.B - c2.B > 130) && (c1.G - c2.G > 130))
        {
            ringCount++;
            if (ringCount == 2)
            {
                radius4 = y - i;
                break;
            }
            i -= 10;
        }
    }

    int centerShiftY = radius3 - radius4;
    if (centerShiftY > 0)
    {
        y += (centerShiftY / 2);
        radius3 -= (centerShiftY / 2);
    }
    else if (centerShiftY < 0)
    {
        y -= (centerShiftY / 2);
        radius3 += (centerShiftY / 2);
    }
} while ((radius != radius2) && (radius3!=radius4));




Pen pen = new Pen(Color.Red, 6);

using (Graphics g = Graphics.FromImage(bmp))
{
    g.DrawEllipse(pen, x - radius, y - radius3, 2 * radius, 2 * radius3);
}
bmp.Save(directory + @"../../../out/out.png");

Console.WriteLine("Plik wynikowy zapisany w folderze out");