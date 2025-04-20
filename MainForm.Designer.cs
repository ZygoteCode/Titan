partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox22 = new System.Windows.Forms.PictureBox();
            this.pictureBox24 = new System.Windows.Forms.PictureBox();
            this.metroTextbox1 = new MetroSuite.MetroTextbox();
            this.metroTextbox2 = new MetroSuite.MetroTextbox();
            this.metroButton1 = new MetroSuite.MetroButton();
            this.metroButton2 = new MetroSuite.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox22
            // 
            this.pictureBox22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox22.BackgroundImage")));
            this.pictureBox22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox22.Location = new System.Drawing.Point(335, 4);
            this.pictureBox22.Name = "pictureBox22";
            this.pictureBox22.Size = new System.Drawing.Size(20, 20);
            this.pictureBox22.TabIndex = 13;
            this.pictureBox22.TabStop = false;
            this.pictureBox22.Click += new System.EventHandler(this.pictureBox22_Click);
            this.pictureBox22.MouseEnter += new System.EventHandler(this.pictureBox22_MouseEnter);
            this.pictureBox22.MouseLeave += new System.EventHandler(this.pictureBox22_MouseLeave);
            // 
            // pictureBox24
            // 
            this.pictureBox24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox24.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox24.BackgroundImage")));
            this.pictureBox24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox24.Location = new System.Drawing.Point(361, 4);
            this.pictureBox24.Name = "pictureBox24";
            this.pictureBox24.Size = new System.Drawing.Size(20, 20);
            this.pictureBox24.TabIndex = 12;
            this.pictureBox24.TabStop = false;
            this.pictureBox24.Click += new System.EventHandler(this.pictureBox24_Click);
            this.pictureBox24.MouseEnter += new System.EventHandler(this.pictureBox24_MouseEnter);
            this.pictureBox24.MouseLeave += new System.EventHandler(this.pictureBox24_MouseLeave);
            // 
            // metroTextbox1
            // 
            this.metroTextbox1.AcceptsReturn = true;
            this.metroTextbox1.AcceptsTab = true;
            this.metroTextbox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.metroTextbox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.metroTextbox1.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.metroTextbox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.metroTextbox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.metroTextbox1.HideSelection = false;
            this.metroTextbox1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.metroTextbox1.LineOnly = true;
            this.metroTextbox1.Location = new System.Drawing.Point(24, 50);
            this.metroTextbox1.MaxLength = 59;
            this.metroTextbox1.Name = "metroTextbox1";
            this.metroTextbox1.PasswordChar = '\0';
            this.metroTextbox1.Size = new System.Drawing.Size(336, 23);
            this.metroTextbox1.Style = MetroSuite.Design.Style.Dark;
            this.metroTextbox1.TabIndex = 14;
            this.metroTextbox1.Text = "Insert the token of the bot here...";
            this.metroTextbox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // metroTextbox2
            // 
            this.metroTextbox2.AcceptsReturn = true;
            this.metroTextbox2.AcceptsTab = true;
            this.metroTextbox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.metroTextbox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.metroTextbox2.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.metroTextbox2.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.metroTextbox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.metroTextbox2.HideSelection = false;
            this.metroTextbox2.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.metroTextbox2.LineOnly = true;
            this.metroTextbox2.Location = new System.Drawing.Point(24, 79);
            this.metroTextbox2.MaxLength = 59;
            this.metroTextbox2.Name = "metroTextbox2";
            this.metroTextbox2.PasswordChar = '\0';
            this.metroTextbox2.Size = new System.Drawing.Size(336, 23);
            this.metroTextbox2.Style = MetroSuite.Design.Style.Dark;
            this.metroTextbox2.TabIndex = 15;
            this.metroTextbox2.Text = "Insert the ID of the user that will use the bot here...";
            this.metroTextbox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // metroButton1
            // 
            this.metroButton1.BackColor = System.Drawing.Color.Transparent;
            this.metroButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.metroButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.metroButton1.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.metroButton1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.metroButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.metroButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.metroButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.metroButton1.Location = new System.Drawing.Point(24, 108);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.metroButton1.RoundingArc = 23;
            this.metroButton1.Size = new System.Drawing.Size(165, 25);
            this.metroButton1.Style = MetroSuite.Design.Style.Dark;
            this.metroButton1.TabIndex = 16;
            this.metroButton1.Text = "Start Nuke Bot";
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.BackColor = System.Drawing.Color.Transparent;
            this.metroButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.metroButton2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.metroButton2.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.metroButton2.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.metroButton2.Enabled = false;
            this.metroButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.metroButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.metroButton2.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.metroButton2.Location = new System.Drawing.Point(195, 108);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.metroButton2.RoundingArc = 23;
            this.metroButton2.Size = new System.Drawing.Size(165, 25);
            this.metroButton2.Style = MetroSuite.Design.Style.Dark;
            this.metroButton2.TabIndex = 17;
            this.metroButton2.Text = "Stop Nuke Bot";
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(385, 158);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroTextbox2);
            this.Controls.Add(this.metroTextbox1);
            this.Controls.Add(this.pictureBox22);
            this.Controls.Add(this.pictureBox24);
            this.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.State = MetroSuite.MetroForm.FormState.Normal;
            this.Style = MetroSuite.Design.Style.Dark;
            this.Text = "Titan V6.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).EndInit();
            this.ResumeLayout(false);

    }

    private System.Windows.Forms.PictureBox pictureBox22;
    private System.Windows.Forms.PictureBox pictureBox24;
    private MetroSuite.MetroTextbox metroTextbox1;
    private MetroSuite.MetroTextbox metroTextbox2;
    private MetroSuite.MetroButton metroButton1;
    private MetroSuite.MetroButton metroButton2;
}