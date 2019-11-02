﻿namespace View
{
    partial class FormDetails
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
            this.buttonChangeElement = new System.Windows.Forms.Button();
            this.buttonDeleteElement = new System.Windows.Forms.Button();
            this.buttonAddElement = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonChangeElement
            // 
            this.buttonChangeElement.Location = new System.Drawing.Point(593, 117);
            this.buttonChangeElement.Name = "buttonChangeElement";
            this.buttonChangeElement.Size = new System.Drawing.Size(191, 45);
            this.buttonChangeElement.TabIndex = 10;
            this.buttonChangeElement.Text = "Изменить";
            this.buttonChangeElement.UseVisualStyleBackColor = true;
            this.buttonChangeElement.Click += new System.EventHandler(this.buttonChangeElement_Click);
            // 
            // buttonDeleteElement
            // 
            this.buttonDeleteElement.Location = new System.Drawing.Point(593, 66);
            this.buttonDeleteElement.Name = "buttonDeleteElement";
            this.buttonDeleteElement.Size = new System.Drawing.Size(191, 45);
            this.buttonDeleteElement.TabIndex = 9;
            this.buttonDeleteElement.Text = "Удалить";
            this.buttonDeleteElement.UseVisualStyleBackColor = true;
            this.buttonDeleteElement.Click += new System.EventHandler(this.buttonDeleteElement_Click);
            // 
            // buttonAddElement
            // 
            this.buttonAddElement.Location = new System.Drawing.Point(593, 15);
            this.buttonAddElement.Name = "buttonAddElement";
            this.buttonAddElement.Size = new System.Drawing.Size(191, 45);
            this.buttonAddElement.TabIndex = 8;
            this.buttonAddElement.Text = "Добавить";
            this.buttonAddElement.UseVisualStyleBackColor = true;
            this.buttonAddElement.Click += new System.EventHandler(this.buttonAddElement_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.Location = new System.Drawing.Point(17, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(554, 426);
            this.dataGridView.TabIndex = 7;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(593, 168);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(191, 44);
            this.buttonUpdate.TabIndex = 22;
            this.buttonUpdate.Text = "Обновить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // FormDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonChangeElement);
            this.Controls.Add(this.buttonDeleteElement);
            this.Controls.Add(this.buttonAddElement);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormDetails";
            this.Text = "Детали";
            this.Load += new System.EventHandler(this.FormDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonChangeElement;
        private System.Windows.Forms.Button buttonDeleteElement;
        private System.Windows.Forms.Button buttonAddElement;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonUpdate;
    }
}