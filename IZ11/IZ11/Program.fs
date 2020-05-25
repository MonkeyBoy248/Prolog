open System
open System.Drawing
open System.Windows.Forms
open System.IO

let form = new Form(Width= 700, Height = 563, Text = "MainMenu",
Menu = new MainMenu())

let mRed = form.Menu.MenuItems.Add("&Left")
let mBlue = form.Menu.MenuItems.Add("&Right")
let image1 = new PictureBox(SizeMode=PictureBoxSizeMode.StretchImage, Top =
5, Width=700,Height=500)
image1.ImageLocation <- "C:\\Texture\Light.png"
form.Controls.Add(image1)

let Form1 = new Form(Width= 700, Height = 500, Text = "Дочерняя форма
№1")
let Edit1 = new TextBox(Text="Вот первая форма",Width=250, Height=250)
let button1 = new Button(Text="OK", Top=40, Width=50, Height=50)
let Exit _ = Form1.Close ()
button1.Click.Add(Exit)
Form1.Controls.Add(Edit1)
Form1.Controls.Add(button1)
let image2 = new PictureBox(SizeMode=PictureBoxSizeMode.StretchImage, Top =
5, Width=700,Height=500)
image2.ImageLocation <- "C:\\Texture\Gul.jpg"
Form1.Controls.Add(image2)

let Form2 = new Form(Width= 700, Height = 500, Text = "Дочерняя форма
№2")
let Edit2 = new TextBox(Text="А вот вторая форма", Width=250, Height=250)
let button2 = new Button(Text="OK", Top=40, Width=50, Height=50)
let Exit1 _ = Form2.Close()
button2.Click.Add(Exit1)
Form2.Controls.Add(Edit2)
Form2.Controls.Add(button2)
let image3 = new PictureBox(SizeMode=PictureBoxSizeMode.StretchImage, Top =
5, Width=700,Height=500)
image3.ImageLocation <- "C:\\Texture\Tom.jpg"
Form2.Controls.Add(image3)


let opForm1 _ = do Form1.ShowDialog()
let opForm2 _ = do Form2.ShowDialog()

let _ = mRed.Click.Add(opForm1)
let _ = mBlue.Click.Add(opForm2)

do Application.Run(form)








