namespace RectanglesDrawing
{
    partial class Scene
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStripScene = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelArea = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelOverlapped = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripScene.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripScene
            // 
            this.statusStripScene.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelArea,
            this.toolStripStatusLabelOverlapped});
            this.statusStripScene.Location = new System.Drawing.Point(0, 344);
            this.statusStripScene.Name = "statusStripScene";
            this.statusStripScene.Size = new System.Drawing.Size(600, 22);
            this.statusStripScene.TabIndex = 0;
            this.statusStripScene.Text = "statusStrip1";
            // 
            // toolStripStatusLabelArea
            // 
            this.toolStripStatusLabelArea.Name = "toolStripStatusLabelArea";
            this.toolStripStatusLabelArea.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabelArea.Text = "Area:";
            // 
            // toolStripStatusLabelOverlapped
            // 
            this.toolStripStatusLabelOverlapped.Name = "toolStripStatusLabelOverlapped";
            this.toolStripStatusLabelOverlapped.Size = new System.Drawing.Size(117, 17);
            this.toolStripStatusLabelOverlapped.Text = "Area not overlapped:";
            // 
            // Scene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.statusStripScene);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Scene";
            this.Text = "Scene";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Scene_FormClosed);
            this.Load += new System.EventHandler(this.Scene_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Delete);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Scene_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scene_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Scene_MouseUp);
            this.statusStripScene.ResumeLayout(false);
            this.statusStripScene.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripScene;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelArea;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelOverlapped;
    }
}

