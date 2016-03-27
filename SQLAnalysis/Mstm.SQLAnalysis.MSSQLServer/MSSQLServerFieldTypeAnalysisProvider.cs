﻿using Mstm.SQLAnalysis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.SQLAnalysis.MSSQLServer
{
    internal class MSSQLServerFieldTypeAnalysisProvider : IFieldTypeAnalysisProvider
    {
        //默认类型
        const FieldTypeEnum _defaultFieldType = FieldTypeEnum.Text;

        //类型枚举字典
        readonly static Dictionary<string, FieldTypeEnum> dict = new Dictionary<string, FieldTypeEnum>()
        {
            //时间
            { "DATE",                      FieldTypeEnum.DateTime        },
            { "TIME",                      FieldTypeEnum.DateTime        },
            { "DATETIME",                  FieldTypeEnum.DateTime        },
            { "DATETIME2",                 FieldTypeEnum.DateTime        },
            { "DATETIMEOFFSET",            FieldTypeEnum.DateTime        },
            { "TIMESTAMP",                 FieldTypeEnum.DateTime        },
            
            //数值
            { "TINYINT",                   FieldTypeEnum.Number          },
            { "SMALLINT",                  FieldTypeEnum.Number          },
            { "INT",                       FieldTypeEnum.Number          },
            { "SMALLDATETIME",             FieldTypeEnum.Number          },
            { "REAL",                      FieldTypeEnum.Number          },
            { "MONEY",                     FieldTypeEnum.Number          },
            { "FLOAT",                     FieldTypeEnum.Number          },
            { "BIGINT",                    FieldTypeEnum.Number          },
            { "BIT",                       FieldTypeEnum.Number          },
            { "DECIMAL",                   FieldTypeEnum.Number          },
            { "NUMERIC",                   FieldTypeEnum.Number          },
            { "SMALLMONEY",                FieldTypeEnum.Number          },

            //字符
            { "NTEXT",                     FieldTypeEnum.Text            },
            { "CHAR",                      FieldTypeEnum.Text            },
            { "BINARY",                    FieldTypeEnum.Text            },
            { "VARCHAR",                   FieldTypeEnum.Text            },
            { "VARBINARY",                 FieldTypeEnum.Text            },
            { "NVARCHAR",                  FieldTypeEnum.Text            },
            { "NCHAR",                     FieldTypeEnum.Text            },
            { "XML",                       FieldTypeEnum.Text            },
            { "SYSNAME",                   FieldTypeEnum.Text            },
            { "TEXT",                      FieldTypeEnum.Text            },
          
            //其他
            { "IMAGE",                     FieldTypeEnum.Text            },
            { "SQL_VARIANT",               FieldTypeEnum.Text            },
            { "UNIQUEIDENTIFIER",          FieldTypeEnum.Text            },
            { "HIERARCHYID",               FieldTypeEnum.Text            },
            { "GEOMETRY",                  FieldTypeEnum.Text            },
            { "GEOGRAPHY",                 FieldTypeEnum.Text            },

        };

        public FieldTypeEnum GetFieldTypeEnum(string fieldType)
        {
            if (string.IsNullOrWhiteSpace(fieldType))
            {
                return _defaultFieldType;
            }
            fieldType = fieldType.ToUpper();
            if (dict.ContainsKey(fieldType))
            {
                return dict[fieldType];
            }
            else
            {
                return _defaultFieldType;
            }
        }
    }
}
