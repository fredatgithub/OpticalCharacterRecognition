using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpticalCharacterRecognition
{
  partial class AboutBoxApplication
  {
    /// <summary>
    /// Variable nécessaire au concepteur.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Nettoyage des ressources utilisées.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Code généré par le Concepteur Windows Form

    /// <summary>
    /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
    /// le contenu de cette méthode avec l'éditeur de code.
    /// </summary>
    private void InitializeComponent()
    {
      ComponentResourceManager resources = new ComponentResourceManager(typeof(AboutBoxApplication));
      this.tableLayoutPanel = new TableLayoutPanel();
      this.logoPictureBox = new PictureBox();
      this.labelProductName = new Label();
      this.labelVersion = new Label();
      this.labelCopyright = new Label();
      this.labelCompanyName = new Label();
      this.textBoxDescription = new TextBox();
      this.okButton = new Button();
      this.tableLayoutPanel.SuspendLayout();
      ((ISupportInitialize)(this.logoPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel
      // 
      this.tableLayoutPanel.ColumnCount = 2;
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
      this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
      this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
      this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
      this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
      this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 1, 3);
      this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 4);
      this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
      this.tableLayoutPanel.Dock = DockStyle.Fill;
      this.tableLayoutPanel.Location = new Point(12, 11);
      this.tableLayoutPanel.Margin = new Padding(4);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.tableLayoutPanel.RowCount = 6;
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      this.tableLayoutPanel.Size = new Size(556, 326);
      this.tableLayoutPanel.TabIndex = 0;
      // 
      // logoPictureBox
      // 
      this.logoPictureBox.Dock = DockStyle.Fill;
      this.logoPictureBox.Image = ((Image)(resources.GetObject("logoPictureBox.Image")));
      this.logoPictureBox.Location = new Point(4, 4);
      this.logoPictureBox.Margin = new Padding(4);
      this.logoPictureBox.Name = "logoPictureBox";
      this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 6);
      this.logoPictureBox.Size = new Size(175, 318);
      this.logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
      this.logoPictureBox.TabIndex = 12;
      this.logoPictureBox.TabStop = false;
      // 
      // labelProductName
      // 
      this.labelProductName.Dock = DockStyle.Fill;
      this.labelProductName.Location = new Point(191, 0);
      this.labelProductName.Margin = new Padding(8, 0, 4, 0);
      this.labelProductName.MaximumSize = new Size(0, 21);
      this.labelProductName.Name = "labelProductName";
      this.labelProductName.Size = new Size(361, 21);
      this.labelProductName.TabIndex = 19;
      this.labelProductName.Text = "Nom du produit";
      this.labelProductName.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelVersion
      // 
      this.labelVersion.Dock = DockStyle.Fill;
      this.labelVersion.Location = new Point(191, 32);
      this.labelVersion.Margin = new Padding(8, 0, 4, 0);
      this.labelVersion.MaximumSize = new Size(0, 21);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new Size(361, 21);
      this.labelVersion.TabIndex = 0;
      this.labelVersion.Text = "Version";
      this.labelVersion.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelCopyright
      // 
      this.labelCopyright.Dock = DockStyle.Fill;
      this.labelCopyright.Location = new Point(191, 64);
      this.labelCopyright.Margin = new Padding(8, 0, 4, 0);
      this.labelCopyright.MaximumSize = new Size(0, 21);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new Size(361, 21);
      this.labelCopyright.TabIndex = 21;
      this.labelCopyright.Text = "Copyright";
      this.labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelCompanyName
      // 
      this.labelCompanyName.Dock = DockStyle.Fill;
      this.labelCompanyName.Location = new Point(191, 96);
      this.labelCompanyName.Margin = new Padding(8, 0, 4, 0);
      this.labelCompanyName.MaximumSize = new Size(0, 21);
      this.labelCompanyName.Name = "labelCompanyName";
      this.labelCompanyName.Size = new Size(361, 21);
      this.labelCompanyName.TabIndex = 22;
      this.labelCompanyName.Text = "Nom de la société";
      this.labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // textBoxDescription
      // 
      this.textBoxDescription.Dock = DockStyle.Fill;
      this.textBoxDescription.Location = new Point(191, 132);
      this.textBoxDescription.Margin = new Padding(8, 4, 4, 4);
      this.textBoxDescription.Multiline = true;
      this.textBoxDescription.Name = "textBoxDescription";
      this.textBoxDescription.ReadOnly = true;
      this.textBoxDescription.ScrollBars = ScrollBars.Both;
      this.textBoxDescription.Size = new Size(361, 155);
      this.textBoxDescription.TabIndex = 23;
      this.textBoxDescription.TabStop = false;
      this.textBoxDescription.Text = "Description";
      // 
      // okButton
      // 
      this.okButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
      this.okButton.DialogResult = DialogResult.Cancel;
      this.okButton.Location = new Point(452, 295);
      this.okButton.Margin = new Padding(4);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(100, 27);
      this.okButton.TabIndex = 24;
      this.okButton.Text = "&OK";
      this.okButton.Click += new EventHandler(this.okButton_Click);
      // 
      // AboutBoxApplication
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new SizeF(8F, 16F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(580, 348);
      this.Controls.Add(this.tableLayoutPanel);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Margin = new Padding(4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutBoxApplication";
      this.Padding = new Padding(12, 11, 12, 11);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "AboutBoxApplication";
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
      ((ISupportInitialize)(this.logoPictureBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel tableLayoutPanel;
    private PictureBox logoPictureBox;
    private Label labelProductName;
    private Label labelVersion;
    private Label labelCopyright;
    private Label labelCompanyName;
    private TextBox textBoxDescription;
    private Button okButton;
  }
}
