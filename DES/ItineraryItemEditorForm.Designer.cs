namespace DES
{
    partial class ItineraryItemEditorForm
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
            this.cbxNodes = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ppgItem = new System.Windows.Forms.PropertyGrid();
            this.lsbItem = new System.Windows.Forms.ListBox();
            this.cbxRVGTypes = new System.Windows.Forms.ComboBox();
            this.lbServiceNode = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxNodes
            // 
            this.cbxNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxNodes.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxNodes.FormattingEnabled = true;
            this.cbxNodes.Location = new System.Drawing.Point(24, 280);
            this.cbxNodes.Name = "cbxNodes";
            this.cbxNodes.Size = new System.Drawing.Size(173, 22);
            this.cbxNodes.TabIndex = 13;
            this.cbxNodes.SelectedIndexChanged += new System.EventHandler(this.cbxNodes_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(376, 399);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 34);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(267, 399);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 34);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Enabled = false;
            this.btnRemove.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(121, 399);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(108, 34);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove Item";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(12, 399);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 34);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add Item";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ppgItem
            // 
            this.ppgItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ppgItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ppgItem.Location = new System.Drawing.Point(254, 23);
            this.ppgItem.Name = "ppgItem";
            this.ppgItem.Size = new System.Drawing.Size(219, 333);
            this.ppgItem.TabIndex = 8;
            // 
            // lsbItem
            // 
            this.lsbItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lsbItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsbItem.FormattingEnabled = true;
            this.lsbItem.ItemHeight = 14;
            this.lsbItem.Location = new System.Drawing.Point(24, 23);
            this.lsbItem.Name = "lsbItem";
            this.lsbItem.Size = new System.Drawing.Size(193, 228);
            this.lsbItem.TabIndex = 7;
            this.lsbItem.SelectedIndexChanged += new System.EventHandler(this.lsbItem_SelectedIndexChanged);
            // 
            // cbxRVGTypes
            // 
            this.cbxRVGTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxRVGTypes.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRVGTypes.FormattingEnabled = true;
            this.cbxRVGTypes.Location = new System.Drawing.Point(24, 334);
            this.cbxRVGTypes.Name = "cbxRVGTypes";
            this.cbxRVGTypes.Size = new System.Drawing.Size(173, 22);
            this.cbxRVGTypes.TabIndex = 14;
            // 
            // lbServiceNode
            // 
            this.lbServiceNode.AutoSize = true;
            this.lbServiceNode.Location = new System.Drawing.Point(21, 263);
            this.lbServiceNode.Name = "lbServiceNode";
            this.lbServiceNode.Size = new System.Drawing.Size(91, 14);
            this.lbServiceNode.TabIndex = 15;
            this.lbServiceNode.Text = "Service Node";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Location = new System.Drawing.Point(23, 317);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(196, 14);
            this.lbType.TabIndex = 16;
            this.lbType.Text = "Service Time Generator Type";
            // 
            // ItineraryItemEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 460);
            this.Controls.Add(this.lbType);
            this.Controls.Add(this.lbServiceNode);
            this.Controls.Add(this.cbxRVGTypes);
            this.Controls.Add(this.cbxNodes);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.ppgItem);
            this.Controls.Add(this.lsbItem);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ItineraryItemEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Itinerary Item Editor Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxNodes;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.PropertyGrid ppgItem;
        private System.Windows.Forms.ListBox lsbItem;
        private System.Windows.Forms.ComboBox cbxRVGTypes;
        private System.Windows.Forms.Label lbServiceNode;
        private System.Windows.Forms.Label lbType;
    }
}