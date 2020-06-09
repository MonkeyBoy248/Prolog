open System
open System.IO
open System.Data
open System.Windows.Forms
open MySql.Data.MySqlClient
    
let EditForm (id:int)=
    let EditorForm = new Form()
    let textBox1 = new System.Windows.Forms.TextBox()
    let textBox2 = new System.Windows.Forms.TextBox()
    let textBox3 = new System.Windows.Forms.TextBox()
    let label1 = new System.Windows.Forms.Label()
    let label2 = new System.Windows.Forms.Label()
    let label3 = new System.Windows.Forms.Label()
    let Continue = new System.Windows.Forms.Button()
    let Cancel = new System.Windows.Forms.Button()
    // 
    // textBox1
    // 
    textBox1.Location <- new System.Drawing.Point(49, 62)
    textBox1.Name <- "textBox1"
    textBox1.Size <- new System.Drawing.Size(143, 20)
    textBox1.TabIndex <- 0
    // 
    // textBox2
    // 
    textBox2.Location <- new System.Drawing.Point(234, 62)
    textBox2.Name <- "textBox2"
    textBox2.Size <- new System.Drawing.Size(143, 20)
    textBox2.TabIndex <- 1
    // 
    //
    // 
    textBox3.Location <- new System.Drawing.Point(422, 62)
    textBox3.Name <- "textBox3"
    textBox3.Size <- new System.Drawing.Size(143, 20)
    textBox3.TabIndex <- 2
    // 
    
    // 
    label1.AutoSize <- true
    label1.Location <- new System.Drawing.Point(46, 46)
    label1.Name <- "label1"
    label1.Size <- new System.Drawing.Size(90, 13)
    label1.TabIndex <- 3
    label1.Text <- "Введите ФИО"
    // 
    
    // 
    label2.AutoSize <- true
    label2.Location <- new System.Drawing.Point(231, 46)
    label2.Name <- "label2"
    label2.Size <- new System.Drawing.Size(111, 13)
    label2.TabIndex <- 4
    label2.Text <- "Введите сумму кредита"
    // 
    
    // 
    label3.AutoSize <- true
    label3.Location <- new System.Drawing.Point(419, 46)
    label3.Name <- "label3"
    label3.Size <- new System.Drawing.Size(170, 13)
    label3.TabIndex <- 5
    label3.Text <- "Введите процентную ставку"
    // 
   
    // 
    Continue.Location <- new System.Drawing.Point(49, 110)
    Continue.Name <- "Continue"
    Continue.Size <- new System.Drawing.Size(236, 70)
    Continue.TabIndex <- 6
    Continue.Text <- "Продолжить"
    Continue.UseVisualStyleBackColor <- true
    // 
    
    // 
    Cancel.Location <- new System.Drawing.Point(336, 110)
    Cancel.Name <- "Cancel"
    Cancel.Size <- new System.Drawing.Size(229, 70)
    Cancel.TabIndex <- 7
    Cancel.Text <- "Отмена"
    Cancel.UseVisualStyleBackColor <- true
     
   
     
    EditorForm.AutoScaleDimensions <- new System.Drawing.SizeF(6.0F, 13.0F)
    EditorForm.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font
    EditorForm.ClientSize <- new System.Drawing.Size(637, 235)
    EditorForm.FormBorderStyle <- System.Windows.Forms.FormBorderStyle.FixedSingle
    EditorForm.Name <- "add_edit"
    EditorForm.Text <- "Редактор записей"

    EditorForm.Controls.Add(Cancel)
    EditorForm.Controls.Add(Continue)
    EditorForm.Controls.Add(label3)
    EditorForm.Controls.Add(label2)
    EditorForm.Controls.Add(label1)
    EditorForm.Controls.Add(textBox3)
    EditorForm.Controls.Add(textBox2)
    EditorForm.Controls.Add(textBox1)

    let connectionStrGlobal = "server=localhost;port=3306;username=root;password=;database=IZ16"

    let ExecuteReader (commandStr:string) (connectionStr:string) = 
        use connection = new MySqlConnection(connectionStr)
        connection.Open()
        use command = new MySqlCommand(commandStr,connection)
        let reader = command.ExecuteReader()
        if(reader.Read()) then
            textBox1.Text <- reader.GetString(0)
            textBox2.Text <- reader.GetString(1)
            textBox3.Text <- reader.GetString(2)
        ()

    let ExecuteCommand (commandStr:string) (connectionStr:string) = 
        use connection = new MySqlConnection(connectionStr)
        connection.Open()
        use command = new MySqlCommand(commandStr,connection)
        command.ExecuteNonQuery() |> ignore
        ()

    let rec pow (m:double) (n:int) =
        let rec pow1 (m:double) (n:int) (result:double)=
            if(n>0) then
                pow1 (m) (n-1) (result*m)
            else
                result
        pow1 m n 1.0
       
    let toDB (loan:int) (persent:double)=
        // (S*m^n*(m-1))/(m^n-1)
        let persentNormal:double = persent/100.0
        let first:double = (Convert.ToDouble(loan)* pow (1.0+persentNormal) 12 * persentNormal)/((pow (1.0+persentNormal) 12) - 1.0)  
        let second:double = (Convert.ToDouble(loan)* pow (1.0+persentNormal) 24 * persentNormal)/(pow (1.0+persentNormal) 24 - 1.0)  
        let third:double = (Convert.ToDouble(loan)* pow (1.0+persentNormal) 36 * persentNormal)/(pow (1.0+persentNormal) 36 - 1.0)  
        let temp=persent.ToString().Replace(",",".")
        if(id = -1) then
            ExecuteCommand ("INSERT INTO `IZ16`.`Loan` (`id`,`FIO`,`Loan`,`Persent`,`First`,`Second`,`Third`) VALUES (NULL,'"+textBox1.Text+"','"+loan.ToString()+"','"+temp+"','"+first.ToString()+"','"+second.ToString()+"','"+third.ToString()+"')") connectionStrGlobal
        else
            ExecuteCommand ("UPDATE `IZ16`.`Loan` SET `FIO`='"+textBox1.Text+"',`Loan`='"+loan.ToString()+"', `Persent`='"+temp+"',`First`='"+first.ToString()+"', `Second`='"+second.ToString()+"', `Third`='"+third.ToString()+"' WHERE `id`='"+id.ToString()+"'") connectionStrGlobal
        ()

    let ContinueClick(e:EventArgs) =
        if(textBox1.TextLength=0 || textBox2.TextLength=0 || textBox3.TextLength=0) then
            MessageBox.Show "Заполните все поля для продолжения" |> ignore
        try
            let loan = Convert.ToInt32(textBox2.Text)
            let persent = Convert.ToDouble(textBox3.Text)
            toDB loan persent
            EditorForm.Close()
            ()
        with
            | :? Exception ->  MessageBox.Show "Ошибка с содержанием полей" |> ignore
 
    let CancelClick(e:EventArgs) = 
        EditorForm.Close()
        ()

    let FormLoad(e:EventArgs) =
        if(id <> -1) then
            ExecuteReader ("SELECT `FIO`,`Loan`,`Persent` FROM `IZ16`.`Loan` WHERE `id`="+id.ToString()) connectionStrGlobal
        ()

    Continue.Click.Add(ContinueClick) 
    Cancel.Click.Add(CancelClick)
    EditorForm.Load.Add(FormLoad)
    EditorForm.ShowDialog()
    ()

let MainWindow() = 
    let mainForm = new Form()
    let dataGridView1 = new System.Windows.Forms.DataGridView();
    let Add = new System.Windows.Forms.Button();
    let Edit = new System.Windows.Forms.Button();
    let Delete = new System.Windows.Forms.Button();
    let id = new System.Windows.Forms.DataGridViewTextBoxColumn();
    let FIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
    let Loan = new System.Windows.Forms.DataGridViewTextBoxColumn();
    let Persent = new System.Windows.Forms.DataGridViewTextBoxColumn();
    let First = new System.Windows.Forms.DataGridViewTextBoxColumn();
    let Second = new System.Windows.Forms.DataGridViewTextBoxColumn();
    let Third= new System.Windows.Forms.DataGridViewTextBoxColumn();
    let panel1 = new System.Windows.Forms.Panel();
    //
    // dataGridView1
    //
    dataGridView1.AllowUserToAddRows <- false;
    dataGridView1.AllowUserToDeleteRows <- false;
    dataGridView1.Anchor <- (System.Windows.Forms.AnchorStyles.Top ||| System.Windows.Forms.AnchorStyles.Bottom ||| System.Windows.Forms.AnchorStyles.Left ||| System.Windows.Forms.AnchorStyles.Right);
    dataGridView1.ColumnHeadersHeightSizeMode <- System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    dataGridView1.Location <- new System.Drawing.Point(12, 12);
    dataGridView1.MultiSelect <- false;
    dataGridView1.Name <- "dataGridView1";
    dataGridView1.ReadOnly <- true;
    dataGridView1.SelectionMode <- System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
    dataGridView1.Size <- new System.Drawing.Size(760, 478);
    dataGridView1.TabIndex <- 0;
    dataGridView1.Columns.Add("id","id")
    dataGridView1.Columns.Add("FIO","ФИО")
    dataGridView1.Columns.Add("Loan","Сумма Кредита") 
    dataGridView1.Columns.Add("Persent","Процентная ставка"); 
    dataGridView1.Columns.Add("First","За 1 год")
    dataGridView1.Columns.Add("Second","За 2 года")
    dataGridView1.Columns.Add("Third","За 3 года")
    
    
    // 
    // Add
    //     
    Add.Location <- new System.Drawing.Point(5, 3);
    Add.Name <- "Add";
    Add.Size <- new System.Drawing.Size(226, 86);
    Add.TabIndex <- 1;
    Add.Text <- "Добавить запись";
    Add.UseVisualStyleBackColor <- true;    
    // 
    // Edit
    //     
    Edit.Location <- new System.Drawing.Point(5, 95);
    Edit.Name <- "Edit";
    Edit.Size <- new System.Drawing.Size(226, 86);
    Edit.TabIndex <- 2;
    Edit.Text <- "Изменить запись";
    Edit.UseVisualStyleBackColor <- true;
    // 
    // Delete
    // 
    Delete.Location <- new System.Drawing.Point(5, 187);
    Delete.Name <- "Delete";
    Delete.Size <- new System.Drawing.Size(226, 86);
    Delete.TabIndex <- 3;
    Delete.Text <- "Удалить запись";
    Delete.UseVisualStyleBackColor <- true;
    // 
    // id
    // 
    id.HeaderText <- "id";
    id.Name <- "id";
    id.ReadOnly <- true;
    // 
    // FIO
    // 
    FIO.HeaderText <- "ФИО";
    FIO.Name <- "FIO";
    FIO.ReadOnly <- true;
    // 
    // Loan
    // 
    Loan.HeaderText <- "Сумма Кредита";
    Loan.Name <- "Loan";
    Loan.ReadOnly <- true;
    // 
    // Persent
    // 
    Persent.HeaderText <- "Процентная ставка";
    Persent.Name <- "Persent";
    Persent.ReadOnly <- true;
    //
    // First
    //
    First.HeaderText <- "1 год";
    First.Name <- "First";
    First.ReadOnly <- true;
    // 
    // Second
    // 
    Second.HeaderText <- "2 года";
    Second.Name <- "Second";
    Second.ReadOnly <- true;
    // 
    // Third
    // 
    Third.HeaderText <- "On 200m";
    Third.Name <- "On200";
    Third.ReadOnly <- true;
    // 
    // panel1
    // 
    panel1.Anchor <- (System.Windows.Forms.AnchorStyles.Top ||| System.Windows.Forms.AnchorStyles.Right);
    panel1.Location <- new System.Drawing.Point(779, 12);
    panel1.Name <- "panel1";
    panel1.Size <- new System.Drawing.Size(233, 281);
    panel1.TabIndex <- 4;
    // 
    // Form1
    //
    mainForm.AutoScaleDimensions <- new System.Drawing.SizeF(6.0F, 13.0F);
    mainForm.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font;
    mainForm.ClientSize <- new System.Drawing.Size(1028, 502);
    mainForm.FormBorderStyle <- System.Windows.Forms.FormBorderStyle.Sizable;
    mainForm.Name <- "Form1";
    mainForm.Text <- "Кредитный калькулятор(ИЗ-16)";
    
    mainForm.Controls.Add(panel1);
    mainForm.Controls.Add(dataGridView1);
    panel1.Controls.Add(Add);
    panel1.Controls.Add(Delete);
    panel1.Controls.Add(Edit);
    
    let connectionStrGlobal = "server=localhost;port=3306;username=root;password=;database=IZ16"   
    
    let ExecuteCommand (commandStr:string) (connectionStr:string) = 
        use connection = new MySqlConnection(connectionStr)
        connection.Open()
        use command = new MySqlCommand(commandStr,connection)
        command.ExecuteNonQuery()
        ()    

    let ExecuteReader (commandStr:string) (connectionStr:string) = 
        use connection = new MySqlConnection(connectionStr)
        connection.Open()
        use command = new MySqlCommand(commandStr,connection)
        let reader = command.ExecuteReader()
        while (reader.Read()) do
            dataGridView1.Rows.Add(reader.GetString(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetString(4),reader.GetString(5),reader.GetString(6))
        ()

    let DataGridViewRefresh = 
        dataGridView1.Rows.Clear()
        ExecuteReader "SELECT * FROM `IZ16`.`Loan`" connectionStrGlobal
        dataGridView1.ClearSelection()
        ()
    
    let formLoad(e:EventArgs) =
        DataGridViewRefresh
        dataGridView1.ClearSelection()
        
    let AddClick(e:EventArgs) = 
        EditForm(-1)
        dataGridView1.Rows.Clear()
        ExecuteReader "SELECT * FROM `IZ16`.`Loan`" connectionStrGlobal
        dataGridView1.ClearSelection()
    
    let EditClick(e:EventArgs) = 
        if(dataGridView1.RowCount=0) then
            MessageBox.Show "Текущая база данных пуста" |> ignore
            else
                if(dataGridView1.SelectedRows.Count<>0) then
                    EditForm(Convert.ToInt32(dataGridView1.SelectedRows.[0].Cells.[0].Value))
                    //DataGridViewRefresh
                    dataGridView1.Rows.Clear()
                    ExecuteReader "SELECT * FROM `IZ16`.`Loan`" connectionStrGlobal
                    dataGridView1.ClearSelection()
                else
                    MessageBox.Show "Выберите запись для редактирования" |> ignore
    
    let DeleteClick(e:EventArgs) =  
        if(dataGridView1.RowCount=0) then
            MessageBox.Show "Текущая база данных пуста"
            ()
            else
                if(dataGridView1.SelectedRows.Count<>0) then
                    ExecuteCommand ("DELETE FROM `IZ16`.`Loan` WHERE `id`="+Convert.ToString(dataGridView1.SelectedRows.[0].Cells.[0].Value)) connectionStrGlobal
                    //DataGridViewRefresh
                    dataGridView1.Rows.Clear()
                    ExecuteReader "SELECT * FROM `IZ16`.`Loan`" connectionStrGlobal
                    dataGridView1.ClearSelection()
                else
                    MessageBox.Show "Выберите запись для редактирования" |> ignore    
        
    let DGVClearSelection(e:EventArgs)=
        dataGridView1.ClearSelection()
    
    Delete.Click.Add(DeleteClick)
    mainForm.Load.Add(formLoad)         
    Add.Click.Add(AddClick)
    Edit.Click.Add(EditClick)
    dataGridView1.DoubleClick.Add(DGVClearSelection)

    mainForm.ShowDialog()
    ()

[<EntryPoint>]
let main argv =
    let ExecuteCommand (commandStr:string) (connectionStr:string) = 
        use connection = new MySqlConnection(connectionStr)
        connection.Open()
        use command = new MySqlCommand(commandStr,connection)
        command.ExecuteNonQuery()
        ()    

    
    ExecuteCommand ("CREATE DATABASE IF NOT EXISTS `IZ16`") ("server=localhost;port=3306;username=root;password=;")
    ExecuteCommand ("CREATE TABLE IF NOT EXISTS `Loan` (`id` INT(11) NOT NULL primary key AUTO_INCREMENT UNIQUE, `FIO` VARCHAR(150) NOT NULL, `Loan` INT(11), `Persent` FLOAT(11), `First` INT(11),`Second` INT(11), `Third` INT(11))") ("server=localhost;port=3306;username=root;password=;database=IZ16")
    MainWindow()
    0 