using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/*****************************************************************************************************************************************
 * ***************************************************************************************************************************************
/****************************************************************************************************************************************/
/*************************************   D O C U M E N T A T I O N    O F     C O D E    ****************************************************
 * *****************************************************************************************************************************************
 * *****************************************************************************************************************************************
 *firstly our code will be generated in a simple richbox text in which will be the input of the asm_function constructor()
 * the code is simply a text that will go ahead into multiple phases :-
 * first phase it will be splitted at every ' ' and then stored into array of string
 * second phase it will remove any comment in the code 
 * then the next phase will generate a symbol table from 2 columbs [ symbol name , value ]
 * the next phase will substitute the symbols with their values and remove the symbol definition lines
 * the next phase will substitute the instructions with their op-code values and for every singly instruction 
 * it will divide its op-code into 2 bytes then will delete nulls and spaces in code 
 * thats it 
 * ***************************************************************************************************************************
 * ***************************************************************************************************************************
 * *********************************************  T H A N K    Y O U  ********************************************************
 * ***************************************************************************************************************************
 * ***************************************************************************************************************************
 * */
/*****************************************************targets*********************************************/
/*
            1/generate build files and use it in the main ui (done)
            2/ make control functions that control memory words an cpu in main ui (done)
            3/ make next-previous buttons codes (done)
            4/ say good bye bye

 */
namespace Morris_emui
{
   public class ASM_function
   {
        /**************************************************************************************************/
        //******************D E F I N I T I O N    O F     I N S T R U C T I O N    S E T******************
        /**************************************************************************************************/
        private string[,] ins_set = { { "AND", "0", " " }, { "AND", "8", "I" },
                                      { "ADD", "1", " " }, { "ADD", "9", "I" },
                                      { "LDA", "2", " " }, { "LDA", "A", "I" },
                                      { "STA", "3", " " }, { "STA", "B", "I" },
                                      { "BUN", "4", " " }, { "BUN", "C", "I" },
                                      { "BSA", "5", " " }, { "BSA", "D", "I" },
                                      { "ISZ", "6", " " }, { "ISZ", "E", "I" },
                                      { "CLA", "7800", " " }, { "CLE", "7400", " " },
                                      { "CMA", "7200", " " }, { "CME", "7100", " " },
                                      { "CIR", "7080", " " }, { "CIL", "7040", " " },
                                      { "INC", "7020", " " }, { "SPA", "7010", " " },
                                      { "SNA", "7008", " " }, { "SZA", "7004", " " },
                                      { "SZE", "7002", " " }, { "HLT", "7001", " " },
                                      { "INP", "F800", " " }, { "OUT", "F400", " " },
                                      { "SKI", "F200", " " }, { "SKO", "F100", " " },
                                      { "ION", "F080", " " }, { "IOF", "F040", " " } };
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /***********************************************************************************************************************
        ******************************************        D E F I N I T I O N S        ****************************************/
        /*********************************************************************************************************************/
         private string[] single_inst = { "HLT", "CMA", "CLA", "CLE",/*******************single instructions******************/
                                          "CMA", "CME", "INC", "CIR",
                                          "CIL", "SPA", "SNA", "SZA",
                                          "SZE", "INP", "OUT", "SKI",
                                          "SKO"
                                        };
         private string[] ind_spl_code = null;
         private string[,] bin_code = new string[4096,2]; private string[] err_msg = null;
         private int ctr; private string[,] ST = new string[1000, 2];
         private int B_ADDRESS = 0;
        /*******************************constructor will receive a string from text box*************************/
        public ASM_function(string x)
        {
            //recieve strng  code and set ind_spl_code 
            ind_spl_code = x.Split('\n');
            //remove the comments in code
            Remove_comments();
            a_path1();
            a_path2();




        }
        /************************************************************************************************************************************/
        /**************************************************  R E M O V E    C O M M E N T S  ***********************************************************************/
        /************************************************************************************************************************************/
        public void Remove_comments()//this function will remove the comments in the code 
        {
            for (int i = 0; i < ind_spl_code.Length; i++)
            {
                string g = ind_spl_code[i]; int v = 9898;
                v = g.IndexOf('/');
                if (v != 9898 && v > 0)
                {
                    g = g.Remove(v);
                    ind_spl_code[i] = g;
                    //MessageBox.Show(RSC[i]);
                }
            }
        }
        //public string[,] cbg()//this is test function that test if code is splitted or not and to delet all comments in the raw code
        //{
        //    a_path1();
        //    a_path2();
        //    return bin_code;
        //    ;
        //}
        /************************************************************************************************************************************/
        /**************************************************  P A T H 1  ***********************************************************************/
        /************************************************************************************************************************************/
        public void a_path1()//this will generates symbol table and delet loop symbols 
        {

            for (int x = 0; x < ind_spl_code.Length; x++)
            {
                //search every symbol in the code array
                string r; r = ind_spl_code[x]; bool w = r.Contains(',');

                if (w)
                {
                    int s = r.IndexOf(',');//find index of ,
                    r = r.Replace(',', ' ');//replace 
                    string[] m = r.Split(' ');//splitting 
                    ST[ctr, 0] = m[0];//store symbol name
                    /////////delete the symbol in the array code
                    if (m[2] != "HEX" && m[2] != "BIN" && m[2] != "DEC")//in case of loop symbol will save the address
                    {
                        ST[ctr, 1] = Convert.ToString(x - 1); ctr++; string k = " ";
                        for (int v = 2; v < m.Length; v++)//delet loop symbol in first indx
                        {
                            if (k == " ") { k = m[v]; }
                            else { k = k + ' ' + m[v]; }
                        }
                        ind_spl_code[x] = k;

                    }
                    else
                    {
                        switch (m[2])//converting no. from hex or bin
                        {
                            case "HEX":
                                ST[ctr, 1] = Convert.ToString(Convert.ToInt32(m[3], 16)); ctr++;
                                break;
                            case "BIN":
                                ST[ctr, 1] = Convert.ToString(Convert.ToInt32(m[3], 2)); ctr++;
                                break;
                            case "DEC":
                                ST[ctr, 1] = m[3]; ctr++;
                                break;
                            default://will send error message method s_err(x)

                                break;
                        }
                    }

                }
            }
            ctr = 0;
        }
        /************************************************************************************************************************************/
        /**************************************************  P A T H 2  ***********************************************************************/
        /************************************************************************************************************************************/
        public void a_path2()//assigns bin []
        {
            string[,] tmp = new string[4096, 3];
            ///////////////////////////////////////////////   
            for (int i = 0; i < ind_spl_code.Length; i++)//remove symbols lines (OK)
            {
                string q = ind_spl_code[i];
                for (int j = 0; j < 1000; j++)
                {
                    if (ST[j, 0] != null)
                    {
                        if (q.Contains(','))
                        {
                            if (q.Contains(ST[j, 0]))
                            {
                                ind_spl_code[i] = " ";
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < ind_spl_code.Length; i++) //seprate every line of code  (OK)
            {
                string p = ind_spl_code[i];
                string[] o = p.Split(' ');

                for (int j = 0; j < o.Length; j++)
                {
                    if (j == 2) break;
                    tmp[i, j] = o[j];
                }
            }
            for (int i = 0; i < ind_spl_code.Length; i++)//substitute symbols with their values (OK)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (tmp[i, 1] == ST[j, 0])
                    {
                        tmp[i, 1] = ST[j, 1];
                    }
                    else
                    {
                        //will send error message err_msg(i)
                    }
                }
            }
            for (int i = 0; i < ind_spl_code.Length; i++)//substitute insructions with their values
            {
                for (int j = 0; j < 32; j++)
                {
                    string a = tmp[i, 0]; string b = ins_set[j, 0];
                    string c = tmp[i, 2]; string d = ins_set[j, 2];
                    if (a == null) c = "  ";
                    if (b == null) c = "  ";
                    if (c == null) c = "  ";
                    if (d == null) c = "  ";
                    if (a.Contains(b) && c.Contains(d))
                    {
                        //assign op-codes of instructions  
                        tmp[i, 0] = ins_set[j, 1]; tmp[i, 2] = " ";
                        //divide single instructions into 2 bytes
                        for (int ss = 0; ss < 17; ss++)
                        {
                            if (a.Contains(single_inst[ss]))
                            {
                                // int tmpp = Convert.ToInt16(ins_set[j, 1]);
                                string ff = ins_set[j, 1];
                                char[] trash = new char[4];
                                trash = ff.ToCharArray();
                                char[] t1 = new char[2];
                                char[] t2 = new char[2];
                                for (int e = 0; e < 2; e++)
                                {
                                    t2[e] = trash[e];

                                }
                                for (int e = 0; e < 2; e++)
                                {
                                    t1[e] = trash[e + 2];

                                }
                                string tt1 = new string(t2);
                                string tt2 = new string(t1);
                                tmp[i, 0] = tt1;
                                tmp[i, 1] = tt2;
                            }
                        }
                    }
                }
                if (tmp[i, 0] == "ORG")
                {
                    B_ADDRESS = Convert.ToInt32(tmp[i, 1]);
                    tmp[i, 0] = " ";
                }
                else
                {
                    //will send error message at i
                }
            }
            int x = 0, y = 0;
            for (int i = 0; i < ind_spl_code.Length; i++)//assign bin 
            {
                int r = 20;
                for (int j = 0; j < 2; j++)
                {
                    if ((tmp[i, j] == null) || (tmp[i, j] == " ") || (tmp[i, j] == "  ") || (tmp[i, j] == "")) { r = 80; }
                    if (r == 20)
                    {
                        bin_code[x, y] = tmp[i, j];
                        y++; if (y == 2) { x++; y = 0; }
                        r = 20;
                    }
                }
            }
        }
       
        public string[,] Get_code_BIN()
        {
            return bin_code;
        }

        public string[] Get_code_instructions()
        {
            int x = 0, y = 0;string[] tmp = new string[4096];
            for (int i = 0; i < ind_spl_code.Length; i++)//assign bin 
            {
                int r = 20;
               
                
                    if ((ind_spl_code[i] == null) || (ind_spl_code[i] == " ") || (ind_spl_code[i] == "  ") || (ind_spl_code[i] == "")) { r = 80; }
                    if (r == 20)
                    {
                        tmp[x] = ind_spl_code[i];
                        x++; 
                        r = 20;
                    }
                
            }
            return tmp;
        }
        public int Get_Base_Address()
        {
            return B_ADDRESS;
        }
        public void GenerateBuildFiles()
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter("codetext.build");
            //Write of text
            // if we used writeline() there will be some errors
            string[] tmp = Get_code_instructions();
            string ss="";
            foreach(string a in tmp)
            {
                ss += a;
            }

                //Close the file
                sw.Write(ss);
            sw.Close();
            /******************************************************/
            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw2 = new StreamWriter("codebin.build");
            //Write of text
            // if we used writeline() there will be some errors
            string[,] tmp2 = Get_code_BIN();
            string ss2 = "";
            foreach (string a in tmp2)
            {
                ss2 += a;
            }

            //Close the file
            sw2.Write(ss2);
            sw2.Close();

        }
    }

}

