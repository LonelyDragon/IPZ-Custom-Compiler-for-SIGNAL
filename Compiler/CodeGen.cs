﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class LinkedVariableTable
    {
        public string Name { get; set; }
        public int Reg { get; set; }
        public int Status { get; set; }
    }
    public class CodeGen
    {
        public StringBuilder Result { get; set; }
        private HashSet<string> variavleTable = new HashSet<string>();
        private HashSet<string> procedureTable = new HashSet<string>();
        private List<LinkedVariableTable> linkedVariables = new List<LinkedVariableTable>(); 
        public void Generate(Tree tree)
        {
            Result = new StringBuilder();
            Result.Append(".386\n.code\nstart:\n");
        }

        private void GenerateCodeForNode(Tree tree)
        {
            if (tree.Name != "statement")
            {
                foreach (var subnode in tree.InnerTree)
                {
                    GenerateCodeForNode(subnode);
                }
            }
            else
            {
                if (tree.InnerTree[1].Name == ":")
                {
                    Result.Append(tree.InnerTree[0].InnerTree[0].Name + ": ");
                    GenerateCodeForNode(tree.InnerTree[2]);
                }
                if (tree.InnerTree[1].Name == ":=")
                {
                    var variable = tree.InnerTree[0].InnerTree[0].Name;
                    var value = tree.InnerTree[2].InnerTree[0].Name;
                    if (procedureTable.Contains(variable))
                    {
                        Result.Append("Name already used for procedure");
                    }
                    else
                    {
                        foreach (var el in linkedVariables.Where(el => el.Name == variable && el.Status == 1))
                        {
                            Result.Append("Variable is a readonly");
                        }
                    }
                        variavleTable.Add(variable);
                    Result.Append("mov ax," + value + "\n");
                    Result.Append("mov " + variable + ",ax\n");
                }
                if (tree.InnerTree[1].Name == "actual-arguments")
                {
                    
                }
            }
        }

        private void PushArguments(Tree tree)
        {
            do
            {

            } while (tree.InnerTree[2].Name == "actual-arguments-list");
        } 
    }
}