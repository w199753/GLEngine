namespace GLEngine
{
    partial class Form1
    {
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		/// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Codice generato da Progettazione Windows Form

		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
            this.RenderControl = new OpenGL.GlControl();
            //this.spControl = new GLEngine.SpTextEditorComponent();
            this.SuspendLayout();
            // 
            // RenderControl
            // 
            this.RenderControl.Animation = true;
            this.RenderControl.AnimationTimer = false;
            this.RenderControl.BackColor = System.Drawing.Color.DimGray;
            this.RenderControl.ColorBits = ((uint)(24u));
            this.RenderControl.DepthBits = ((uint)(0u));
            this.RenderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenderControl.Location = new System.Drawing.Point(0, 0);
            this.RenderControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.RenderControl.MultisampleBits = ((uint)(0u));
            this.RenderControl.Name = "RenderControl";
            this.RenderControl.Size = new System.Drawing.Size(853, 494);
            this.RenderControl.StencilBits = ((uint)(0u));
            this.RenderControl.TabIndex = 0;
            this.RenderControl.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextCreated);
            this.RenderControl.ContextDestroying += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextDestroying);
            this.RenderControl.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_Render);
            this.RenderControl.ContextUpdate += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextUpdate);
            this.RenderControl.Paint += new System.Windows.Forms.PaintEventHandler(this.RenderControl_ContextPaint);
            //this.RenderControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPresshhh);
            //// 
            //// spControl
            //// 
            //this.spControl.Location = new System.Drawing.Point(0, 0);
            //this.spControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            //this.spControl.Name = "spControl";
            //this.spControl.Size = new System.Drawing.Size(125, 125);
            //this.spControl.TabIndex = 1;
            //this.spControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPresshhh);

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 494);
            //this.Controls.Add(this.spControl);
            this.Controls.Add(this.RenderControl);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Hello triangle";
            this.ResumeLayout(false);

		}

		#endregion

		private OpenGL.GlControl RenderControl;
		private SpTextEditorComponent spControl;
    }
}
