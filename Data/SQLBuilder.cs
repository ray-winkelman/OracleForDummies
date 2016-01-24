﻿using System.Collections.Generic;
using System.Text;

namespace OFD.Data
{
    /// <summary>
    /// This class builds both DDL and DML statements in the PL-SQL programming language using common conventions. 
    /// </summary>
    public class SQLBuilder
    {
        public SQLBuilder()
        {

        }

        public static string GetCreateTableStatement(string tablename, Dictionary<string, string> columns)
        {
            string delimiter = "";
            StringBuilder statement = new StringBuilder("CREATE TABLE " + tablename + " (");

            foreach (KeyValuePair<string, string> column in columns)
            {
                if (column.Equals("id"))
                {
                    statement.AppendLine(delimiter + column.Key + ' ' + column.Value + " GENERATED ALWAYS AS IDENTITY");
                }
                else
                {
                    statement.AppendLine(delimiter + column.Key + ' ' + column.Value);
                }

                delimiter = ", ";
            }

            statement.AppendLine(", TIME_INSERTED DATE DEFAULT SYSDATE NOT NULL");
            statement.AppendLine(", TIME_UPDATED DATE DEFAULT SYSDATE NOT NULL");
            statement.AppendLine(")");

            return statement.ToString();
        }

        public static string GetInsertStatement(string tablename, Dictionary<string, string> colval)
        {
            string delimiter = string.Empty;

            StringBuilder statement = new StringBuilder("INSERT INTO " + tablename + " (");
            StringBuilder values = new StringBuilder(") VALUES (");

            foreach (KeyValuePair<string, string> column in colval)
            {
                // Skip the ID because it's auto-incrementing.
                if (column.Equals("id"))
                {
                    continue;
                }

                statement.Append(delimiter + column.Key);
                values.Append(delimiter + column.Value);

                delimiter = ", ";
            }

            values.Append(")");

            return statement.ToString() + values.ToString();
        }
    }
}

