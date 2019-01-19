using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelToJSON
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 저장 위치 설정
            string strAppURLPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            strAppURLPath += "\\data.json";
            tbSaveFile.Text = strAppURLPath.Substring(6);
        }


        DataSet ImportExcelXLS(string FileName, bool hasHeaders)
        {
            //This connection string works if you have Office 2007+ installed and your 
            //data is saved in a .xlsx file
            var strConn = String.Format(@"
                Provider=Microsoft.ACE.OLEDB.12.0;
                Data Source={0};
                Extended Properties=""Excel 12.0 Xml;HDR=YES""
            ", FileName);

            DataSet output = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable schemaTable = 
                conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow schemaRow in schemaTable.Rows)
                {
                    //string sheet = schemaRow["TABLE_NAME"].ToString();
                    string sheet = "Sheet1";

                    if (!sheet.EndsWith("_"))
                    {
                        try
                        {
                            //OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                            var cmd = conn.CreateCommand();
                            cmd.CommandText = String.Format(@"SELECT * FROM [{0}$]", sheet);

                            cmd.CommandType = CommandType.Text;

                            DataTable outputTable = new DataTable(sheet);

                            string strTableName = System.IO.Path.GetFileNameWithoutExtension(FileName);
                            outputTable.TableName = strTableName;

                            output.Tables.Add(outputTable);
                            new OleDbDataAdapter(cmd).Fill(outputTable);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        conn.Close();
                    }
                    break;  //쉬트 하나만 쓸 것이다.
                }
            }
            return output;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConvertExcelToJson(tbSaveFile.Text.ToString());
        }

        void ConvertExcelToJson(string strSaveFileName)
        {
            progressBar1.Value = 20;

            if (tbFindPath.Text.ToString().Length == 0)
            {
                MessageBox.Show("String is empty");
                return;
            }

            DataSet ds = ImportExcelXLS(tbFindPath.Text.ToString(), false);

            progressBar1.Value = 50;

            var json = JsonConvert.SerializeObject(ds);
            if (json.Length < 3)
            {
                MessageBox.Show("Can't read this file");
                return;
            }

            progressBar1.Value = 70;

            File.WriteAllText(strSaveFileName, json);

            progressBar1.Value = 100;

            string strMessage = "Save" + " at " + "[" + strSaveFileName + "]";
            MessageBox.Show(strMessage);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnFindFile_Click(object sender, EventArgs e)
        {
            string strAppURLPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string strAppPath = strAppURLPath.Substring(6);

            openFileDialog1.InitialDirectory = strAppPath;
            openFileDialog1.Title = "Select File";
            openFileDialog1.Filter = "xlsxFile|*.xlsx"; //표시할 파일의 확장자 필터 
            openFileDialog1.ShowReadOnly = true; 
            openFileDialog1.Multiselect = true; //파일을 여러개 선택가능하게 함 
            openFileDialog1.FileName = "";
     
            if(openFileDialog1.ShowDialog() == DialogResult.OK) 
            { 
                foreach(string strFile in openFileDialog1.FileNames) 
                {
                   tbFindPath.Text = strFile;

                   string strSaveDir = System.IO.Path.GetDirectoryName(strFile);
                   tbSaveFile.Text = strSaveDir + "\\" + System.IO.Path.GetFileNameWithoutExtension(strFile) + ".txt";                  

                   break;  //하나만 읽는다.
                }
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            string strAppURLPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string strAppPath = strAppURLPath.Substring(6);

            saveFileDialog1.InitialDirectory = strAppPath;
            saveFileDialog1.Filter = "jsonFile|*.json"; ;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ConvertExcelToJson(saveFileDialog1.FileName);
                tbSaveFile.Text = saveFileDialog1.FileName;
            }
        }
    }
}