namespace MB.VS.Extension.CommentMyCode.UserOptions.Controls
{
  partial class MainOptionPageControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.uxTemplateDataTextBox = new System.Windows.Forms.RichTextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.uxMaxColNumUpDown = new System.Windows.Forms.NumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.uxMaxColNumUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 49);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(108, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "File Header Template";
      // 
      // uxTemplateDataTextBox
      // 
      this.uxTemplateDataTextBox.AcceptsTab = true;
      this.uxTemplateDataTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.uxTemplateDataTextBox.Location = new System.Drawing.Point(6, 65);
      this.uxTemplateDataTextBox.Name = "uxTemplateDataTextBox";
      this.uxTemplateDataTextBox.Size = new System.Drawing.Size(337, 262);
      this.uxTemplateDataTextBox.TabIndex = 1;
      this.uxTemplateDataTextBox.Text = "";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 10);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(92, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Maximum Column:";
      // 
      // uxMaxColNumUpDown
      // 
      this.uxMaxColNumUpDown.Location = new System.Drawing.Point(102, 10);
      this.uxMaxColNumUpDown.Name = "uxMaxColNumUpDown";
      this.uxMaxColNumUpDown.Size = new System.Drawing.Size(42, 20);
      this.uxMaxColNumUpDown.TabIndex = 3;
      this.uxMaxColNumUpDown.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
      // 
      // MainOptionPageControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.uxMaxColNumUpDown);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.uxTemplateDataTextBox);
      this.Controls.Add(this.label1);
      this.Name = "MainOptionPageControl";
      this.Size = new System.Drawing.Size(346, 330);
      ((System.ComponentModel.ISupportInitialize)(this.uxMaxColNumUpDown)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.RichTextBox uxTemplateDataTextBox;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.NumericUpDown uxMaxColNumUpDown;
  }
}
