using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Vic.Data
{
    /// <summary>
    /// 数据类型类
    /// </summary>
    [SuppressMessage("Rule Category", "CS1591")]
    [Serializable]
    public static class DbTypes
    {
        #region 数据库类型转 System.Data.DbType 方法
        /// <summary>
        /// 将 Vic.Data.SqlDbType 转换为 System.Data.DbType
        /// </summary>
        /// <param name="dbType">Vic.Data.SqlDbType</param>
        /// <returns></returns>
        public static System.Data.DbType SqlParse(SqlDbType dbType)
        {
            System.Data.DbType type = new System.Data.DbType();
            switch (dbType)
            {
                case SqlDbType.BigInt:
                    type = System.Data.DbType.Int64;
                    break;
                case SqlDbType.Binary:
                    type = System.Data.DbType.Binary;
                    break;
                case SqlDbType.Bit:
                    type = System.Data.DbType.Boolean;
                    break;
                case SqlDbType.Char:
                    type = System.Data.DbType.AnsiStringFixedLength;
                    break;
                case SqlDbType.Date:
                    type = System.Data.DbType.Date;
                    break;
                case SqlDbType.DateTime:
                    type = System.Data.DbType.DateTime;
                    break;
                case SqlDbType.DateTime2:
                    type = System.Data.DbType.DateTime2;
                    break;
                case SqlDbType.DateTimeOffset:
                    type = System.Data.DbType.DateTimeOffset;
                    break;
                case SqlDbType.Decimal:
                    type = System.Data.DbType.Decimal;
                    break;
                case SqlDbType.Float:
                    type = System.Data.DbType.Double;
                    break;
                case SqlDbType.Image:
                    type = System.Data.DbType.Binary;
                    break;
                case SqlDbType.Int:
                    type = System.Data.DbType.Int32;
                    break;
                case SqlDbType.Money:
                    type = System.Data.DbType.Currency;
                    break;
                case SqlDbType.NChar:
                    type = System.Data.DbType.StringFixedLength;
                    break;
                case SqlDbType.NText:
                    type = System.Data.DbType.String;
                    break;
                case SqlDbType.NVarChar:
                    type = System.Data.DbType.String;
                    break;
                case SqlDbType.Real:
                    type = System.Data.DbType.Single;
                    break;
                case SqlDbType.SmallDateTime:
                    type = System.Data.DbType.DateTime;
                    break;
                case SqlDbType.SmallInt:
                    type = System.Data.DbType.Int16;
                    break;
                case SqlDbType.SmallMoney:
                    type = System.Data.DbType.Currency;
                    break;
                case SqlDbType.Structured:
                    type = System.Data.DbType.Object;
                    break;
                case SqlDbType.Text:
                    type = System.Data.DbType.AnsiString;
                    break;
                case SqlDbType.Time:
                    type = System.Data.DbType.Time;
                    break;
                case SqlDbType.Timestamp:
                    type = System.Data.DbType.Binary;
                    break;
                case SqlDbType.TinyInt:
                    type = System.Data.DbType.Byte;
                    break;
                case SqlDbType.Udt:
                    type = System.Data.DbType.Object;
                    break;
                case SqlDbType.UniqueIdentifier:
                    type = System.Data.DbType.Guid;
                    break;
                case SqlDbType.VarBinary:
                    type = System.Data.DbType.Binary;
                    break;
                case SqlDbType.VarChar:
                    type = System.Data.DbType.AnsiString;
                    break;
                case SqlDbType.Variant:
                    type = System.Data.DbType.Object;
                    break;
                case SqlDbType.Xml:
                    type = System.Data.DbType.Xml;
                    break;
            }
            return type;
        }

        /// <summary>
        /// 将 Vic.Data.OdbcType 转换为 System.Data.DbType
        /// </summary>
        /// <param name="dbType">OdbcType</param>
        /// <returns></returns>
        public static System.Data.DbType OdbcParse(OdbcType dbType)
        {
            System.Data.DbType type = new System.Data.DbType();
            switch (dbType)
            {
                case OdbcType.BigInt:
                    type = System.Data.DbType.Int64;
                    break;
                case OdbcType.Binary:
                    type = System.Data.DbType.Binary;
                    break;
                case OdbcType.Bit:
                    type = System.Data.DbType.Boolean;
                    break;
                case OdbcType.Char:
                    type = System.Data.DbType.AnsiStringFixedLength;
                    break;
                case OdbcType.DateTime:
                    type = System.Data.DbType.DateTime;
                    break;
                case OdbcType.Decimal:
                    type = System.Data.DbType.Decimal;
                    break;
                case OdbcType.Numeric:
                    type = System.Data.DbType.Decimal;
                    break;
                case OdbcType.Double:
                    type = System.Data.DbType.Double;
                    break;
                case OdbcType.Image:
                    type = System.Data.DbType.Binary;
                    break;
                case OdbcType.Int:
                    type = System.Data.DbType.Int32;
                    break;
                case OdbcType.NChar:
                    type = System.Data.DbType.StringFixedLength;
                    break;
                case OdbcType.NText:
                    type = System.Data.DbType.String;
                    break;
                case OdbcType.NVarChar:
                    type = System.Data.DbType.String;
                    break;
                case OdbcType.Real:
                    type = System.Data.DbType.Single;
                    break;
                case OdbcType.UniqueIdentifier:
                    type = System.Data.DbType.Guid;
                    break;
                case OdbcType.SmallDateTime:
                    type = System.Data.DbType.DateTime;
                    break;
                case OdbcType.SmallInt:
                    type = System.Data.DbType.Int16;
                    break;
                case OdbcType.Text:
                    type = System.Data.DbType.AnsiString;
                    break;
                case OdbcType.Timestamp:
                    type = System.Data.DbType.Binary;
                    break;
                case OdbcType.TinyInt:
                    type = System.Data.DbType.Byte;
                    break;
                case OdbcType.VarBinary:
                    type = System.Data.DbType.Binary;
                    break;
                case OdbcType.VarChar:
                    type = System.Data.DbType.AnsiString;
                    break;
                case OdbcType.Date:
                    type = System.Data.DbType.Date;
                    break;
                case OdbcType.Time:
                    type = System.Data.DbType.Time;
                    break;
            }
            return type;
        }

        /// <summary>
        /// 将 Vic.Data.OleDbType 转换为 System.Data.DbType
        /// </summary>
        /// <param name="dbType">OleDbType</param>
        /// <returns></returns>
        public static System.Data.DbType OleDbParse(OleDbType dbType)
        {
            System.Data.DbType type = new System.Data.DbType();
            switch (dbType)
            {
                case OleDbType.Empty:
                    type = System.Data.DbType.Object;
                    break;
                case OleDbType.SmallInt:
                    type = System.Data.DbType.Int16;
                    break;
                case OleDbType.Integer:
                    type = System.Data.DbType.Int32;
                    break;
                case OleDbType.Single:
                    type = System.Data.DbType.Single;
                    break;
                case OleDbType.Double:
                    type = System.Data.DbType.Double;
                    break;
                case OleDbType.Currency:
                    type = System.Data.DbType.Currency;
                    break;
                case OleDbType.Date:
                    type = System.Data.DbType.DateTime;
                    break;
                case OleDbType.BSTR:
                    type = System.Data.DbType.String;
                    break;
                case OleDbType.IDispatch:
                    type = System.Data.DbType.Object;
                    break;
                case OleDbType.Error:
                    type = System.Data.DbType.Int32;
                    break;
                case OleDbType.Boolean:
                    type = System.Data.DbType.Boolean;
                    break;
                case OleDbType.Variant:
                    type = System.Data.DbType.Object;
                    break;
                case OleDbType.IUnknown:
                    type = System.Data.DbType.Object;
                    break;
                case OleDbType.Decimal:
                    type = System.Data.DbType.Decimal;
                    break;
                case OleDbType.TinyInt:
                    type = System.Data.DbType.SByte;
                    break;
                case OleDbType.UnsignedTinyInt:
                    type = System.Data.DbType.Byte;
                    break;
                case OleDbType.UnsignedSmallInt:
                    type = System.Data.DbType.UInt16;
                    break;
                case OleDbType.UnsignedInt:
                    type = System.Data.DbType.UInt32;
                    break;
                case OleDbType.BigInt:
                    type = System.Data.DbType.Int64;
                    break;
                case OleDbType.UnsignedBigInt:
                    type = System.Data.DbType.UInt64;
                    break;
                case OleDbType.Filetime:
                    type = System.Data.DbType.DateTime;
                    break;
                case OleDbType.Guid:
                    type = System.Data.DbType.Guid;
                    break;
                case OleDbType.Binary:
                    type = System.Data.DbType.Binary;
                    break;
                case OleDbType.Char:
                    type = System.Data.DbType.AnsiStringFixedLength;
                    break;
                case OleDbType.WChar:
                    type = System.Data.DbType.StringFixedLength;
                    break;
                case OleDbType.Numeric:
                    type = System.Data.DbType.Decimal;
                    break;
                case OleDbType.DBDate:
                    type = System.Data.DbType.Date;
                    break;
                case OleDbType.DBTime:
                    type = System.Data.DbType.Time;
                    break;
                case OleDbType.DBTimeStamp:
                    type = System.Data.DbType.DateTime;
                    break;
                case OleDbType.PropVariant:
                    type = System.Data.DbType.Object;
                    break;
                case OleDbType.VarNumeric:
                    type = System.Data.DbType.VarNumeric;
                    break;
                case OleDbType.VarChar:
                    type = System.Data.DbType.AnsiString;
                    break;
                case OleDbType.LongVarChar:
                    type = System.Data.DbType.AnsiString;
                    break;
                case OleDbType.VarWChar:
                    type = System.Data.DbType.String;
                    break;
                case OleDbType.LongVarWChar:
                    type = System.Data.DbType.String;
                    break;
                case OleDbType.VarBinary:
                    type = System.Data.DbType.Binary;
                    break;
                case OleDbType.LongVarBinary:
                    type = System.Data.DbType.Binary;
                    break;
            }
            return type;
        }

        /// <summary>
        /// 将 Vic.Data.OracleType 转换为 System.Data.DbType
        /// </summary>
        /// <param name="dbType">OracleType</param>
        /// <returns></returns>
        public static System.Data.DbType OracleParse(OracleType dbType)
        {
            System.Data.DbType type = new System.Data.DbType();
            switch (dbType)
            {
                case OracleType.BFile:
                    type = System.Data.DbType.Binary;
                    break;
                case OracleType.Blob:
                    type = System.Data.DbType.Binary;
                    break;
                case OracleType.Char:
                    type = System.Data.DbType.AnsiStringFixedLength;
                    break;
                case OracleType.Clob:
                    type = System.Data.DbType.AnsiString;
                    break;
                case OracleType.Cursor:
                    type = System.Data.DbType.Object;
                    break;
                case OracleType.DateTime:
                    type = System.Data.DbType.DateTime;
                    break;
                case OracleType.IntervalDayToSecond:
                    type = System.Data.DbType.Object;
                    break;
                case OracleType.IntervalYearToMonth:
                    type = System.Data.DbType.Int32;
                    break;
                case OracleType.LongRaw:
                    type = System.Data.DbType.Binary;
                    break;
                case OracleType.LongVarChar:
                    type = System.Data.DbType.AnsiString;
                    break;
                case OracleType.NChar:
                    type = System.Data.DbType.StringFixedLength;
                    break;
                case OracleType.NClob:
                    type = System.Data.DbType.String;
                    break;
                case OracleType.Number:
                    type = System.Data.DbType.VarNumeric;
                    break;
                case OracleType.NVarChar:
                    type = System.Data.DbType.String;
                    break;
                case OracleType.Raw:
                    type = System.Data.DbType.Binary;
                    break;
                case OracleType.RowId:
                    type = System.Data.DbType.AnsiString;
                    break;
                case OracleType.Timestamp:
                    type = System.Data.DbType.DateTime;
                    break;
                case OracleType.TimestampLocal:
                    type = System.Data.DbType.DateTime;
                    break;
                case OracleType.TimestampWithTZ:
                    type = System.Data.DbType.DateTime;
                    break;
                case OracleType.VarChar:
                    type = System.Data.DbType.AnsiString;
                    break;
                case OracleType.Byte:
                    type = System.Data.DbType.Byte;
                    break;
                case OracleType.UInt16:
                    type = System.Data.DbType.UInt16;
                    break;
                case OracleType.UInt32:
                    type = System.Data.DbType.UInt32;
                    break;
                case OracleType.SByte:
                    type = System.Data.DbType.SByte;
                    break;
                case OracleType.Int16:
                    type = System.Data.DbType.Int16;
                    break;
                case OracleType.Int32:
                    type = System.Data.DbType.Int32;
                    break;
                case OracleType.Float:
                    type = System.Data.DbType.Single;
                    break;
                case OracleType.Double:
                    type = System.Data.DbType.Double;
                    break;
            }
            return type;
        }

        /// <summary>
        /// 将 Vic.Data.OracleClientType 转换为 System.Data.DbType
        /// </summary>
        /// <param name="dbType">OracleClientType</param>
        /// <returns></returns>
        public static System.Data.DbType OracleClientParse(OracleClientType dbType)
        {
            System.Data.DbType type = new System.Data.DbType();
            switch (dbType)
            {
                //case OracleClientType.Array:
                //    type = System.Data.DbType.Object;
                //    break;
                //case OracleClientType.BFile:
                //    type = System.Data.DbType.Object;
                //    break;
                //case OracleClientType.BinaryDouble:
                //    type = System.Data.DbType.Double;
                //    break;
                //case OracleClientType.BinaryFloat:
                //    type = System.Data.DbType.Single;
                //    break;
                case OracleClientType.Blob:
                    type = System.Data.DbType.Object;
                    break;
                case OracleClientType.Byte:
                    type = System.Data.DbType.Byte;
                    break;
                case OracleClientType.Char:
                    type = System.Data.DbType.StringFixedLength;
                    break;
                case OracleClientType.Clob:
                    type = System.Data.DbType.Object;
                    break;
                case OracleClientType.Date:
                    type = System.Data.DbType.Date;
                    break;
                case OracleClientType.Decimal:
                    type = System.Data.DbType.Decimal;
                    break;
                case OracleClientType.Double:
                    type = System.Data.DbType.Double;
                    break;
                case OracleClientType.Int16:
                    type = System.Data.DbType.Int16;
                    break;
                case OracleClientType.Int32:
                    type = System.Data.DbType.Int32;
                    break;
                case OracleClientType.Int64:
                    type = System.Data.DbType.Int64;
                    break;
                //case OracleClientType.IntervalDS:
                //    type = System.Data.DbType.Object;
                //    break;
                //case OracleClientType.IntervalYM:
                //    type = System.Data.DbType.Int64;
                //    break;
                case OracleClientType.Long:
                    type = System.Data.DbType.String;
                    break;
                //case OracleClientType.LongRaw:
                //    type = System.Data.DbType.Binary;
                //    break;
                case OracleClientType.NChar:
                    type = System.Data.DbType.StringFixedLength;
                    break;
                case OracleClientType.NClob:
                    type = System.Data.DbType.Object;
                    break;
                case OracleClientType.NVarchar2:
                    type = System.Data.DbType.String;
                    break;
                //case OracleClientType.Object:
                //    type = System.Data.DbType.Object;
                //    break;
                //case OracleClientType.Raw:
                //    type = System.Data.DbType.Binary;
                //    break;
                //case OracleClientType.Ref:
                //    type = System.Data.DbType.Object;
                //    break;
                case OracleClientType.RefCursor:
                    type = System.Data.DbType.Object;
                    break;
                //case OracleClientType.Single:
                //    type = System.Data.DbType.Single;
                //    break;
                case OracleClientType.TimeStamp:
                    type = System.Data.DbType.DateTime;
                    break;
                //case OracleClientType.TimeStampLTZ:
                //    type = System.Data.DbType.DateTime;
                //    break;
                //case OracleClientType.TimeStampTZ:
                //    type = System.Data.DbType.DateTime;
                //    break;
                case OracleClientType.Varchar2:
                    type = System.Data.DbType.String;
                    break;
                case OracleClientType.XmlType:
                    type = System.Data.DbType.String;
                    break;
            }
            return type;
        }
        #endregion

        #region 数据库类型、C# 数据类型转换方法
        /// <summary>
        /// 将 Vic.Data.SqlDbType 转换为 C# 数据类型
        /// </summary>
        /// <param name="sqlType">SqlDbType</param>
        /// <returns></returns>
        public static Type ToCsharpType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(Int64);
                case SqlDbType.Binary:
                    return typeof(object);
                case SqlDbType.Bit:
                    return typeof(bool);
                case SqlDbType.Char:
                    return typeof(string);
                case SqlDbType.DateTime:
                    return typeof(DateTime);
                case SqlDbType.Decimal:
                    return typeof(Decimal);
                case SqlDbType.Float:
                    return typeof(double);
                case SqlDbType.Image:
                    return typeof(object);
                case SqlDbType.Int:
                    return typeof(int);
                case SqlDbType.Money:
                    return typeof(decimal);
                case SqlDbType.NChar:
                    return typeof(string);
                case SqlDbType.NText:
                    return typeof(string);
                case SqlDbType.NVarChar:
                    return typeof(string);
                case SqlDbType.Real:
                    return typeof(Single);
                case SqlDbType.SmallDateTime:
                    return typeof(DateTime);
                case SqlDbType.SmallInt:
                    return typeof(Int16);
                case SqlDbType.SmallMoney:
                    return typeof(decimal);
                case SqlDbType.Text:
                    return typeof(string);
                case SqlDbType.Timestamp:
                    return typeof(object);
                case SqlDbType.TinyInt:
                    return typeof(byte);
                case SqlDbType.Udt:
                    return typeof(object);
                case SqlDbType.UniqueIdentifier:
                    return typeof(object);
                case SqlDbType.VarBinary:
                    return typeof(object);
                case SqlDbType.VarChar:
                    return typeof(string);
                case SqlDbType.Variant:
                    return typeof(object);
                case SqlDbType.Xml:
                    return typeof(object);
                default:
                    return null;
            }
        }
        #endregion

        #region 直接返回数据库类型对应的 System.Data.DbType
        /// <summary>
        /// 适用于 System.Data.SqlClient 驱动
        /// </summary>
        public static class Sql
        {
            public static System.Data.DbType BigInt
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.BigInt); }
            }
            public static System.Data.DbType Binary
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Binary); }
            }
            public static System.Data.DbType Bit
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Bit); }
            }
            public static System.Data.DbType Char
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Char); }
            }
            public static System.Data.DbType Date
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Date); }
            }
            public static System.Data.DbType DateTime
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.DateTime); }
            }
            public static System.Data.DbType DateTime2
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.DateTime2); }
            }
            public static System.Data.DbType DateTimeOffset
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.DateTimeOffset); }
            }
            public static System.Data.DbType Decimal
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Decimal); }
            }
            public static System.Data.DbType Float
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Float); }
            }
            public static System.Data.DbType Image
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Image); }
            }
            public static System.Data.DbType Int
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Int); }
            }
            public static System.Data.DbType Money
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Money); }
            }
            public static System.Data.DbType NChar
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.NChar); }
            }
            public static System.Data.DbType NText
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.NText); }
            }
            public static System.Data.DbType NVarChar
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.NVarChar); }
            }
            public static System.Data.DbType Real
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Real); }
            }
            public static System.Data.DbType SmallDateTime
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.SmallDateTime); }
            }
            public static System.Data.DbType SmallInt
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.SmallInt); }
            }
            public static System.Data.DbType SmallMoney
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.SmallMoney); }
            }
            public static System.Data.DbType Structured
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Structured); }
            }
            public static System.Data.DbType Text
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Text); }
            }
            public static System.Data.DbType Time
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Time); }
            }
            public static System.Data.DbType Timestamp
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Timestamp); }
            }
            public static System.Data.DbType TinyInt
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.TinyInt); }
            }
            public static System.Data.DbType Udt
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Udt); }
            }
            public static System.Data.DbType UniqueIdentifier
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.UniqueIdentifier); }
            }
            public static System.Data.DbType VarBinary
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.VarBinary); }
            }
            public static System.Data.DbType VarChar
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.VarChar); }
            }
            public static System.Data.DbType Variant
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Variant); }
            }
            public static System.Data.DbType Xml
            {
                get { return DbTypes.SqlParse(Vic.Data.SqlDbType.Xml); }
            }
        }

        /// <summary>
        /// 适用于 System.Data.Odbc 驱动
        /// </summary>
        public static class Odbc
        {
            public static System.Data.DbType BigInt
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.BigInt); }
            }
            public static System.Data.DbType Binary
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Binary); }
            }
            public static System.Data.DbType Bit
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Bit); }
            }
            public static System.Data.DbType Char
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Char); }
            }
            public static System.Data.DbType DateTime
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.DateTime); }
            }
            public static System.Data.DbType Decimal
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Decimal); }
            }
            public static System.Data.DbType Numeric
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Numeric); }
            }
            public static System.Data.DbType Double
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Double); }
            }
            public static System.Data.DbType Image
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Image); }
            }
            public static System.Data.DbType Int
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Int); }
            }
            public static System.Data.DbType NChar
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.NChar); }
            }
            public static System.Data.DbType NText
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.NText); }
            }
            public static System.Data.DbType NVarChar
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.NVarChar); }
            }
            public static System.Data.DbType Real
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Real); }
            }
            public static System.Data.DbType UniqueIdentifier
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.UniqueIdentifier); }
            }
            public static System.Data.DbType SmallDateTime
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.SmallDateTime); }
            }
            public static System.Data.DbType SmallInt
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.SmallInt); }
            }
            public static System.Data.DbType Text
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Text); }
            }
            public static System.Data.DbType Timestamp
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Timestamp); }
            }
            public static System.Data.DbType TinyInt
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.TinyInt); }
            }
            public static System.Data.DbType VarBinary
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.VarBinary); }
            }
            public static System.Data.DbType VarChar
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.VarChar); }
            }
            public static System.Data.DbType Date
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Date); }
            }
            public static System.Data.DbType Time
            {
                get { return DbTypes.OdbcParse(Vic.Data.OdbcType.Time); }
            }
        }

        /// <summary>
        /// 适用于 System.Data.OleDb 驱动
        /// </summary>
        public static class OleDb
        {
            public static System.Data.DbType Empty
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Empty); }
            }
            public static System.Data.DbType SmallInt
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.SmallInt); }
            }
            public static System.Data.DbType Integer
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Integer); }
            }
            public static System.Data.DbType Single
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Single); }
            }
            public static System.Data.DbType Double
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Double); }
            }
            public static System.Data.DbType Currency
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Currency); }
            }
            public static System.Data.DbType Date
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Date); }
            }
            public static System.Data.DbType BSTR
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.BSTR); }
            }
            public static System.Data.DbType IDispatch
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.IDispatch); }
            }
            public static System.Data.DbType Error
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Error); }
            }
            public static System.Data.DbType Boolean
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Boolean); }
            }
            public static System.Data.DbType Variant
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Variant); }
            }
            public static System.Data.DbType IUnknown
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.IUnknown); }
            }
            public static System.Data.DbType Decimal
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Decimal); }
            }
            public static System.Data.DbType TinyInt
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.TinyInt); }
            }
            public static System.Data.DbType UnsignedTinyInt
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.UnsignedTinyInt); }
            }
            public static System.Data.DbType UnsignedSmallInt
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.UnsignedSmallInt); }
            }
            public static System.Data.DbType UnsignedInt
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.UnsignedInt); }
            }
            public static System.Data.DbType BigInt
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.BigInt); }
            }
            public static System.Data.DbType UnsignedBigInt
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.UnsignedBigInt); }
            }
            public static System.Data.DbType Filetime
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Filetime); }
            }
            public static System.Data.DbType Guid
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Guid); }
            }
            public static System.Data.DbType Binary
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Binary); }
            }
            public static System.Data.DbType Char
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Char); }
            }
            public static System.Data.DbType WChar
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.WChar); }
            }
            public static System.Data.DbType Numeric
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Decimal); }
            }
            public static System.Data.DbType DBDate
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.Date); }
            }
            public static System.Data.DbType DBTime
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.DBTime); }
            }
            public static System.Data.DbType DBTimeStamp
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.DBTimeStamp); }
            }
            public static System.Data.DbType PropVariant
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.PropVariant); }
            }
            public static System.Data.DbType VarNumeric
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.VarNumeric); }
            }
            public static System.Data.DbType VarChar
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.VarChar); }
            }
            public static System.Data.DbType LongVarChar
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.LongVarChar); }
            }
            public static System.Data.DbType VarWChar
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.VarWChar); }
            }
            public static System.Data.DbType LongVarWChar
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.LongVarWChar); }
            }
            public static System.Data.DbType VarBinary
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.VarBinary); }
            }
            public static System.Data.DbType LongVarBinary
            {
                get { return DbTypes.OleDbParse(Vic.Data.OleDbType.LongVarBinary); }
            }
        }

        /// <summary>
        /// 适用于 System.Data.OracleClient 驱动
        /// </summary>
        public static class Oracle
        {
            public static System.Data.DbType BFile
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.BFile); }
            }
            public static System.Data.DbType Blob
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Blob); }
            }
            public static System.Data.DbType Char
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Char); }
            }
            public static System.Data.DbType Clob
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Clob); }
            }
            public static System.Data.DbType Cursor
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Cursor); }
            }
            public static System.Data.DbType DateTime
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.DateTime); }
            }
            public static System.Data.DbType IntervalDayToSecond
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.IntervalDayToSecond); }
            }
            public static System.Data.DbType IntervalYearToMonth
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.IntervalYearToMonth); }
            }
            public static System.Data.DbType LongRaw
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.LongRaw); }
            }
            public static System.Data.DbType LongVarChar
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.LongVarChar); }
            }
            public static System.Data.DbType NChar
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.NChar); }
            }
            public static System.Data.DbType NClob
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.NClob); }
            }
            public static System.Data.DbType Number
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Number); }
            }
            public static System.Data.DbType NVarChar
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.NVarChar); }
            }
            public static System.Data.DbType Raw
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Raw); }
            }
            public static System.Data.DbType RowId
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.RowId); }
            }
            public static System.Data.DbType Timestamp
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Timestamp); }
            }
            public static System.Data.DbType TimestampLocal
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.TimestampLocal); }
            }
            public static System.Data.DbType TimestampWithTZ
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.TimestampWithTZ); }
            }
            public static System.Data.DbType VarChar
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.VarChar); }
            }
            public static System.Data.DbType Byte
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Byte); }
            }
            public static System.Data.DbType UInt16
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.UInt16); }
            }
            public static System.Data.DbType UInt32
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.UInt32); }
            }
            public static System.Data.DbType SByte
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.SByte); }
            }
            public static System.Data.DbType Int16
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Int16); }
            }
            public static System.Data.DbType Int32
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Int32); }
            }
            public static System.Data.DbType Float
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Float); }
            }
            public static System.Data.DbType Double
            {
                get { return DbTypes.OracleParse(Vic.Data.OracleType.Double); }
            }
        }

        /// <summary>
        /// 适用于 Oracle.DataAccess.Client、Oracle.ManagedDataAccess.Client 驱动
        /// </summary>
        public static class OracleClient
        {
            //public static System.Data.DbType Array
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Array); }
            //}
            //public static System.Data.DbType BFile
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.BFile); }
            //}
            //public static System.Data.DbType BinaryDouble
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.BinaryDouble); }
            //}
            //public static System.Data.DbType BinaryFloat
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.BinaryFloat); }
            //}
            public static System.Data.DbType Blob
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Blob); }
            }
            public static System.Data.DbType Byte
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Byte); }
            }
            public static System.Data.DbType Char
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Char); }
            }
            public static System.Data.DbType Clob
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Clob); }
            }
            public static System.Data.DbType Date
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Date); }
            }
            public static System.Data.DbType Decimal
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Decimal); }
            }
            public static System.Data.DbType Double
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Double); }
            }
            public static System.Data.DbType Int16
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Int16); }
            }
            public static System.Data.DbType Int32
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Int32); }
            }
            public static System.Data.DbType Int64
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Int64); }
            }
            //public static System.Data.DbType IntervalDS
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.IntervalDS); }
            //}
            //public static System.Data.DbType IntervalYM
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.IntervalYM); }
            //}
            public static System.Data.DbType Long
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Long); }
            }
            //public static System.Data.DbType LongRaw
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.LongRaw); }
            //}
            public static System.Data.DbType NChar
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.NChar); }
            }
            public static System.Data.DbType NClob
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.NClob); }
            }
            public static System.Data.DbType NVarchar2
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.NVarchar2); }
            }
            //public static System.Data.DbType Object
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Object); }
            //}
            //public static System.Data.DbType Raw
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Raw); }
            //}
            //public static System.Data.DbType Ref
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Ref); }
            //}
            public static System.Data.DbType RefCursor
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.RefCursor); }
            }
            //public static System.Data.DbType Single
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Single); }
            //}
            public static System.Data.DbType TimeStamp
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.TimeStamp); }
            }
            //public static System.Data.DbType TimeStampLTZ
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.TimeStampLTZ); }
            //}
            //public static System.Data.DbType TimeStampTZ
            //{
            //    get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.TimeStampTZ); }
            //}
            public static System.Data.DbType Varchar2
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.Varchar2); }
            }
            public static System.Data.DbType XmlType
            {
                get { return DbTypes.OracleClientParse(Vic.Data.OracleClientType.XmlType); }
            }
        }
        #endregion
    }

    #region 数据库类型枚举
    /// <summary>
    /// 用于 Vic.Data.DataAccess 中 Parameter 参数 SQL Server 特定的数据类型
    /// </summary>
    [SuppressMessage("Rule Category", "CS1591")]
    [Serializable]
    public enum SqlDbType
    {
        //
        // 摘要:
        //     System.Int64.64 位带符号整数。
        BigInt = 0,
        //
        // 摘要:
        //     System.Byte 类型的 System.Array。二进制数据的固定长度流，范围在 1 到 8,000 个字节之间。
        Binary = 1,
        //
        // 摘要:
        //     System.Boolean.无符号数值，可以是 0、1 或 null。
        Bit = 2,
        //
        // 摘要:
        //     System.String.非 Unicode 字符的固定长度流，范围在 1 到 8,000 个字符之间。
        Char = 3,
        //
        // 摘要:
        //     System.DateTime.日期和时间数据，值范围从 1753 年 1 月 1 日到 9999 年 12 月 31 日，精度为 3.33 毫秒。
        DateTime = 4,
        //
        // 摘要:
        //     System.Decimal.固定精度和小数位数数值，在 -10 38 -1 和 10 38 -1 之间。
        Decimal = 5,
        //
        // 摘要:
        //     System.Double.-1.79E +308 到 1.79E +308 范围内的浮点数。
        Float = 6,
        //
        // 摘要:
        //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 0 到 2 31 -1（即 2,147,483,647）字节之间。
        Image = 7,
        //
        // 摘要:
        //     System.Int32.32 位带符号整数。
        Int = 8,
        //
        // 摘要:
        //     System.Decimal.货币值，范围在 -2 63（即 -922,337,203,685,477.5808）到 2 63 -1（即 +922,337,203,685,477.5807）之间，精度为千分之十个货币单位。
        Money = 9,
        //
        // 摘要:
        //     System.String.Unicode 字符的固定长度流，范围在 1 到 4,000 个字符之间。
        NChar = 10,
        //
        // 摘要:
        //     System.String.Unicode 数据的可变长度流，最大长度为 2 30 - 1（即 1,073,741,823）个字符。
        NText = 11,
        //
        // 摘要:
        //     System.String.Unicode 字符的可变长度流，范围在 1 到 4,000 个字符之间。如果字符串大于 4,000 个字符，隐式转换会失败。在使用比
        //     4,000 个字符更长的字符串时，请显式设置对象。
        NVarChar = 12,
        //
        // 摘要:
        //     System.Single.-3.40E +38 到 3.40E +38 范围内的浮点数。
        Real = 13,
        //
        // 摘要:
        //     System.Guid.全局唯一标识符（或 GUID）。
        UniqueIdentifier = 14,
        //
        // 摘要:
        //     System.DateTime.日期和时间数据，值范围从 1900 年 1 月 1 日到 2079 年 6 月 6 日，精度为 1 分钟。
        SmallDateTime = 15,
        //
        // 摘要:
        //     System.Int16.16 位的带符号整数。
        SmallInt = 16,
        //
        // 摘要:
        //     System.Decimal.货币值，范围在 -214,748.3648 到 +214,748.3647 之间，精度为千分之十个货币单位。
        SmallMoney = 17,
        //
        // 摘要:
        //     System.String.非 Unicode 数据的可变长度流，最大长度为 2 31 -1（即 2,147,483,647）个字符。
        Text = 18,
        //
        // 摘要:
        //     System.Byte 类型的 System.Array。自动生成的二进制数字，它们保证在数据库中是唯一的。timestamp 通常用作为表行添加版本戳的机制。存储大小为
        //     8 字节。
        Timestamp = 19,
        //
        // 摘要:
        //     System.Byte.8 位无符号整数。
        TinyInt = 20,
        //
        // 摘要:
        //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。如果字节数组大于 8,000
        //     个字节，隐式转换会失败。在使用比 8,000 个字节大的字节数组时，请显式设置对象。
        VarBinary = 21,
        //
        // 摘要:
        //     System.String.非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。
        VarChar = 22,
        //
        // 摘要:
        //     System.Object.特殊数据类型，可以包含数值、字符串、二进制或日期数据，以及 SQL Server 值 Empty 和 Null，后两个值在未声明其他类型的情况下采用。
        Variant = 23,
        //
        // 摘要:
        //     XML 值。使用 System.Data.SqlClient.SqlDataReader.GetValue(System.Int32) 方法或 System.Data.SqlTypes.SqlXml.Value
        //     属性获取字符串形式的 XML，或通过调用 System.Data.SqlTypes.SqlXml.CreateReader() 方法获取 System.Xml.XmlReader
        //     形式的 XML。
        Xml = 25,
        //
        // 摘要:
        //     SQL Server 2005 用户定义的类型 (UDT)。
        Udt = 29,
        //
        // 摘要:
        //     指定表值参数中包含的构造数据的特殊数据类型。
        Structured = 30,
        //
        // 摘要:
        //     日期数据，值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。
        Date = 31,
        //
        // 摘要:
        //     基于 24 小时制的时间数据。时间值范围从 00:00:00 到 23:59:59.9999999，精度为 100 毫微秒。对应于 SQL Server
        //     time 值。
        Time = 32,
        //
        // 摘要:
        //     日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //     100 毫微秒。
        DateTime2 = 33,
        //
        // 摘要:
        //     显示时区的日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //     100 毫微秒。时区值范围从 -14:00 到 +14:00。
        DateTimeOffset = 34,
    }

    /// <summary>
    /// 用于 Vic.Data.DataAccess 中 Parameter 参数 Odbc 特定的数据类型
    /// </summary>
    [SuppressMessage("Rule Category", "CS1591")]
    [Serializable]
    public enum OdbcType
    {
        //
        // 摘要:
        //     确切数值，其精度为 19 （如果有符号）或 20 （如果没有符号），小数位数为 0（有符号：-2[63] <= n <= 2[63] - 1，没有符号：0
        //     <= n <= 2[64] - 1）(SQL_BIGINT)。它映射到 System.Int64。
        BigInt = 1,
        //
        // 摘要:
        //     二进制数据流 (SQL_BINARY)。它映射到 System.Byte 类型的 System.Array。
        Binary = 2,
        //
        // 摘要:
        //     只有一位的二进制数据 (SQL_BIT)。它映射到 System.Boolean。
        Bit = 3,
        //
        // 摘要:
        //     固定长度字符串 (SQL_CHAR)。它映射到 System.String。
        Char = 4,
        //
        // 摘要:
        //     格式为 yyyymmddhhmmss 的日期数据 (SQL_TYPE_TIMESTAMP)。它映射到 System.DateTime。
        DateTime = 5,
        //
        // 摘要:
        //     Signed, exact, numeric value with a precision of at least p and scale s,
        //     where 1 <= p <= 15 and s <= p.The maximum precision is driver-specific (SQL_DECIMAL).它映射到
        //     System.Decimal。
        Decimal = 6,
        //
        // 摘要:
        //     有符号的确切数值，其精度为 p，小数位数为 s，其中 1 <= p <= 15 并且 s <= p (SQL_NUMERIC)。它映射到 System.Decimal。
        Numeric = 7,
        //
        // 摘要:
        //     有符号的近似数值，其二进制精度为 53 （零或绝对值为 10[-308] 到 10[308]） (SQL_DOUBLE)。它映射到 System.Double。
        Double = 8,
        //
        // 摘要:
        //     变长二进制数据。最大长度因数据源而定 (SQL_LONGVARBINARY)。它映射到 System.Byte 类型的 System.Array。
        Image = 9,
        //
        // 摘要:
        //     确切数值，其精度为 10 和小数位数 0（有符号：-2[31] <= n <= 2[31] - 1，没有符号：0 <= n <= 2[32] -
        //     1）(SQL_INTEGER)。它映射到 System.Int32。
        Int = 10,
        //
        // 摘要:
        //     固定长度的 Unicode 字符串 (SQL_WCHAR)。它映射到 System.String。
        NChar = 11,
        //
        // 摘要:
        //     Unicode 变长字符数据。最大长度因数据源而定。(SQL_WLONGVARCHAR)。它映射到 System.String。
        NText = 12,
        //
        // 摘要:
        //     Unicode 字符的变长流 (SQL_WVARCHAR)。它映射到 System.String。
        NVarChar = 13,
        //
        // 摘要:
        //     有符号的近似数值，其二进制精度为 24 （零或绝对值为 10[-38] 到 10[38]）。(SQL_REAL)。它映射到 System.Single。
        Real = 14,
        //
        // 摘要:
        //     固定长度的 GUID (SQL_GUID)。它映射到 System.Guid。
        UniqueIdentifier = 15,
        //
        // 摘要:
        //     格式为 yyyymmddhhmmss 的数据和时间数据 (SQL_TYPE_TIMESTAMP)。它映射到 System.DateTime。
        SmallDateTime = 16,
        //
        // 摘要:
        //     确切数值，其精度为 5，小数位数为 0 （有符号：-32,768 <= n <= 32,767，没有符号：0 <= n <= 65,535）(SQL_SMALLINT)。它映射到
        //     System.Int16。
        SmallInt = 17,
        //
        // 摘要:
        //     变长字符数据。最大长度因数据源而定 (SQL_LONGVARCHAR)。它映射到 System.String。
        Text = 18,
        //
        // 摘要:
        //     二进制数据流 (SQL_BINARY)。它映射到 System.Byte 类型的 System.Array。
        Timestamp = 19,
        //
        // 摘要:
        //     确切数值，其精度为 3，小数位数为 0 （有符号：-128 <= n <= 127，没有符号：0 <= n <= 255）(SQL_TINYINT)。This
        //     maps to System.Byte.
        TinyInt = 20,
        //
        // 摘要:
        //     变长二进制。由用户设置该最大值 (SQL_VARBINARY)。它映射到 System.Byte 类型的 System.Array。
        VarBinary = 21,
        //
        // 摘要:
        //     变长流字符串 (SQL_CHAR)。它映射到 System.String。
        VarChar = 22,
        //
        // 摘要:
        //     格式为 yyyymmdd 的日期数据 (SQL_TYPE_DATE)。它映射到 System.DateTime。
        Date = 23,
        //
        // 摘要:
        //     格式为 hhmmss 的日期数据 (SQL_TYPE_TIMES)。它映射到 System.DateTime。
        Time = 24,
    }

    /// <summary>
    /// 用于 Vic.Data.DataAccess 中 Parameter 参数 OleDb 特定的数据类型
    /// </summary>
    [SuppressMessage("Rule Category", "CS1591")]
    [Serializable]
    public enum OleDbType
    {
        // 摘要:
        //     无任何值 (DBTYPE_EMPTY)。
        Empty = 0,
        //
        // 摘要:
        //     16 位带符号的整数 (DBTYPE_I2)。This maps to System.Int16.
        SmallInt = 2,
        //
        // 摘要:
        //     32 位带符号的整数 (DBTYPE_I4)。它映射到 System.Int32。
        Integer = 3,
        //
        // 摘要:
        //     浮点数字，范围在 -3.40E +38 到 3.40E +38 之间 (DBTYPE_R4)。This maps to System.Single.
        Single = 4,
        //
        // 摘要:
        //     浮点数字，范围在 -1.79E +308 到 1.79E +308 之间 (DBTYPE_R8)。它映射到 System.Double。
        Double = 5,
        //
        // 摘要:
        //     一个货币值，范围在 -2 63（或 -922,337,203,685,477.5808）到 2 63 -1（或 +922,337,203,685,477.5807）之间，精度为千分之十个货币单位
        //     (DBTYPE_CY)。它映射到 System.Decimal。
        Currency = 6,
        //
        // 摘要:
        //     日期数据，存储为双精度型 (DBTYPE_DATE)。整数部分是自 1899 年 12 月 30 日以来的天数，而小数部分是不足一天的部分。它映射到
        //     System.DateTime。
        Date = 7,
        //
        // 摘要:
        //     以 null 终止的 Unicode 字符串 (DBTYPE_BSTR)。This maps to System.String.
        BSTR = 8,
        //
        // 摘要:
        //     指向 IDispatch 接口的指针 (DBTYPE_IDISPATCH)。它映射到 System.Object。
        IDispatch = 9,
        //
        // 摘要:
        //     32 位错误代码 (DBTYPE_ERROR)。它映射到 System.Exception。
        Error = 10,
        //
        // 摘要:
        //     布尔值 (DBTYPE_BOOL)。它映射到 System.Boolean。
        Boolean = 11,
        //
        // 摘要:
        //     可包含数字、字符串、二进制或日期数据以及特殊值 Empty 和 Null 的特殊数据类型 (DBTYPE_VARIANT)。如果未指定任何其他类型，则假定为该类型。This
        //     maps to System.Object.
        Variant = 12,
        //
        // 摘要:
        //     指向 IUnknown 接口的指针 (DBTYPE_UNKNOWN)。它映射到 System.Object。
        IUnknown = 13,
        //
        // 摘要:
        //     定点精度和小数位数数值，范围在 -10 38 -1 和 10 38 -1 之间 (DBTYPE_DECIMAL)。它映射到 System.Decimal。
        Decimal = 14,
        //
        // 摘要:
        //     8 位带符号的整数 (DBTYPE_I1)。This maps to System.SByte.
        TinyInt = 16,
        //
        // 摘要:
        //     8 位无符号整数 (DBTYPE_UI1)。它映射到 System.Byte。
        UnsignedTinyInt = 17,
        //
        // 摘要:
        //     16 位无符号整数 (DBTYPE_UI2)。This maps to System.UInt16.
        UnsignedSmallInt = 18,
        //
        // 摘要:
        //     32 位无符号整数 (DBTYPE_UI4)。This maps to System.UInt32.
        UnsignedInt = 19,
        //
        // 摘要:
        //     64 位带符号的整数 (DBTYPE_I8)。它映射到 System.Int64。
        BigInt = 20,
        //
        // 摘要:
        //     64 位无符号整数 (DBTYPE_UI8)。This maps to System.UInt64.
        UnsignedBigInt = 21,
        //
        // 摘要:
        //     64 位无符号整数，表示自 1601 年 1 月 1 日以来 100 个纳秒间隔的数字 (DBTYPE_FILETIME)。它映射到 System.DateTime。
        Filetime = 64,
        //
        // 摘要:
        //     全局唯一标识符（或 GUID） (DBTYPE_GUID)。它映射到 System.Guid。
        Guid = 72,
        //
        // 摘要:
        //     二进制数据流 (DBTYPE_BYTES)。它映射到 System.Byte 类型的 System.Array。
        Binary = 128,
        //
        // 摘要:
        //     字符串 (DBTYPE_STR)。它映射到 System.String。
        Char = 129,
        //
        // 摘要:
        //     以 null 终止的 Unicode 字符流 (DBTYPE_WSTR)。它映射到 System.String。
        WChar = 130,
        //
        // 摘要:
        //     具有定点精度和小数位数的精确数值 (DBTYPE_NUMERIC)。它映射到 System.Decimal。
        Numeric = 131,
        //
        // 摘要:
        //     格式为 yyyymmdd 的日期数据 (DBTYPE_DBDATE)。它映射到 System.DateTime。
        DBDate = 133,
        //
        // 摘要:
        //     格式为 hhmmss 的时间数据 (DBTYPE_DBTIME)。它映射到 System.TimeSpan。
        DBTime = 134,
        //
        // 摘要:
        //     格式为 yyyymmddhhmmss 的日期和时间数据 (DBTYPE_DBTIMESTAMP)。它映射到 System.DateTime。
        DBTimeStamp = 135,
        //
        // 摘要:
        //     自动化 PROPVARIANT (DBTYPE_PROP_VARIANT)。它映射到 System.Object。
        PropVariant = 138,
        //
        // 摘要:
        //     变长数值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.Decimal。
        VarNumeric = 139,
        //
        // 摘要:
        //     非 Unicode 字符的变长流（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        VarChar = 200,
        //
        // 摘要:
        //     长的字符串值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        LongVarChar = 201,
        //
        // 摘要:
        //     长可变、以 null 终止的 Unicode 字符流（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        VarWChar = 202,
        //
        // 摘要:
        //     长的以 null 终止的 Unicode 字符串值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        LongVarWChar = 203,
        //
        // 摘要:
        //     二进制数据的变长流（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.Byte 类型的 System.Array。
        VarBinary = 204,
        //
        // 摘要:
        //     长的二进制值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.Byte 类型的 System.Array。
        LongVarBinary = 205,
    }

    /// <summary>
    /// 用于 Vic.Data.DataAccess 中 Parameter 参数 Oracle 特定的数据类型
    /// </summary>
    [SuppressMessage("Rule Category", "CS1591")]
    [Serializable]
    public enum OracleType
    {
        // 摘要:
        //     Oracle BFILE 数据类型，它包含存储在外部文件中的最大为 4 GB 的二进制数据的引用。使用具有 System.Data.OracleClient.OracleParameter.Value
        //     属性的 OracleClient System.Data.OracleClient.OracleBFile 数据类型。
        BFile = 1,
        //
        // 摘要:
        //     包含二进制数据的 Oracle BLOB 数据类型，其最大大小为 4 GB。使用 System.Data.OracleClient.OracleParameter.Value
        //     中的 OracleClient System.Data.OracleClient.OracleLob 数据类型。
        Blob = 2,
        //
        // 摘要:
        //     Oracle CHAR 数据类型，它包含最大为 2,000 字节的定长字符串。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.String 或 OracleClient System.Data.OracleClient.OracleString
        //     数据类型。
        Char = 3,
        //
        // 摘要:
        //     包含字符数据的 Oracle CLOB 数据类型，根据服务器的默认字符集，其最大大小为 4 GB。使用 System.Data.OracleClient.OracleParameter.Value
        //     中的 OracleClient System.Data.OracleClient.OracleLob 数据类型。
        Clob = 4,
        //
        // 摘要:
        //     Oracle REF CURSOR。System.Data.OracleClient.OracleDataReader 对象不可用。
        Cursor = 5,
        //
        // 摘要:
        //     An Oracle DATE data type that contains a fixed-length representation of a
        //     date and time, ranging from January 1, 4712 B.C.to December 31, A.D.默认格式为
        //     dd-mmm-yy。For A.D.dates, DATE maps to System.DateTime.To bind B.C.dates,
        //     use a String parameter and the Oracle TO_DATE or TO_CHAR conversion functions
        //     for input and output parameters respectively.在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.DateTime 或 OracleClient System.Data.OracleClient.OracleDateTime
        //     数据类型。
        DateTime = 6,
        //
        // 摘要:
        //     Oracle INTERVAL DAY TO SECOND 数据类型（Oracle 9i 或更高版本），它包含以天、小时、分钟和秒为计量单位的时间间隔，大小固定，为
        //     11 字节。在 System.Data.OracleClient.OracleParameter.Value 中使用 .NET Framework
        //     System.TimeSpan 或 OracleClient System.Data.OracleClient.OracleTimeSpan 数据类型。
        IntervalDayToSecond = 7,
        //
        // 摘要:
        //     Oracle INTERVAL YEAR TO MONTH 数据类型（Oracle 9i 或更高版本），它包含以年和月为单位的时间间隔，大小固定，为
        //     5 字节。在 System.Data.OracleClient.OracleParameter.Value 中使用 .NET Framework
        //     System.Int32 或 OracleClient System.Data.OracleClient.OracleMonthSpan 数据类型。
        IntervalYearToMonth = 8,
        //
        // 摘要:
        //     包含变长二进制数据的 Oracle LONGRAW 数据类型，其最大大小为 2 GB。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework Byte[] 或 OracleClient System.Data.OracleClient.OracleBinary
        //     数据类型。
        LongRaw = 9,
        //
        // 摘要:
        //     Oracle LONG 数据类型，它包含最大为 2 GB 的变长字符串。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.String 或 OracleClient System.Data.OracleClient.OracleString
        //     数据类型。
        LongVarChar = 10,
        //
        // 摘要:
        //     Oracle NCHAR 数据类型，它包含要存储在数据库的区域字符集中的定长字符串，存储在数据库中时最大大小为 2,000 字节（不是字符）。值的大小取决于数据库的区域字符集。有关更多信息，请参见
        //     Oracle 文档。在 System.Data.OracleClient.OracleParameter.Value 中使用 .NET Framework
        //     System.String 或 OracleClient System.Data.OracleClient.OracleString 数据类型。
        NChar = 11,
        //
        // 摘要:
        //     Oracle NCLOB 数据类型，它包含要存储在数据库的区域字符集中的字符数据，存储在数据库中时最大大小为 4 GB（不是字符）。值的大小取决于数据库的区域字符集。有关更多信息，请参见
        //     Oracle 文档。在 System.Data.OracleClient.OracleParameter.Value 中使用 .NET Framework
        //     System.String 或 OracleClient System.Data.OracleClient.OracleString 数据类型。
        NClob = 12,
        //
        // 摘要:
        //     An Oracle NUMBER data type that contains variable-length numeric data with
        //     a maximum precision and scale of 38.This maps to System.Decimal.若要绑定超出 System.Decimal.MaxValue
        //     可包含的大小的 Oracle NUMBER，请使用 System.Data.OracleClient.OracleNumber 数据类型，或为输入参数和输出参数分别使用
        //     String 参数和 Oracle TO_NUMBER 或 TO_CHAR 转换函数。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.Decimal 或 OracleClient System.Data.OracleClient.OracleNumber
        //     数据类型。
        Number = 13,
        //
        // 摘要:
        //     Oracle NVARCHAR2 数据类型，它包含数据库的区域字符集中存储的变长字符串，存储在数据库中时最大大小为 4,000 字节（不是字符）。值的大小取决于数据库的区域字符集。有关更多信息，请参见
        //     Oracle 文档。在 System.Data.OracleClient.OracleParameter.Value 中使用 .NET Framework
        //     System.String 或 OracleClient System.Data.OracleClient.OracleString 数据类型。
        NVarChar = 14,
        //
        // 摘要:
        //     Oracle RAW 数据类型，它包含最大为 2,000 字节的变长二进制数据。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework Byte[] 或 OracleClient System.Data.OracleClient.OracleBinary
        //     数据类型。
        Raw = 15,
        //
        // 摘要:
        //     Oracle ROWID 数据类型的 base64 字符串表示形式。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.String 或 OracleClient System.Data.OracleClient.OracleString
        //     数据类型。
        RowId = 16,
        //
        // 摘要:
        //     Oracle TIMESTAMP（Oracle 9i 或更高版本），它包含日期和时间（包括秒），大小范围从 7 字节到 11 字节。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.DateTime 或 OracleClient System.Data.OracleClient.OracleDateTime
        //     数据类型。
        Timestamp = 18,
        //
        // 摘要:
        //     Oracle TIMESTAMP WITH LOCAL TIMEZONE（Oracle 9i 或更高版本），它包含日期、时间和对原始时区的引用，大小范围从
        //     7 字节到 11 字节。在 System.Data.OracleClient.OracleParameter.Value 中使用 .NET Framework
        //     System.DateTime 或 OracleClient System.Data.OracleClient.OracleDateTime 数据类型。
        TimestampLocal = 19,
        //
        // 摘要:
        //     Oracle TIMESTAMP WITH TIMEZONE（Oracle 9i 或更高版本），它包含日期、时间和指定时区，大小固定，为 13 字节。在
        //     System.Data.OracleClient.OracleParameter.Value 中使用 .NET Framework System.DateTime
        //     或 OracleClient System.Data.OracleClient.OracleDateTime 数据类型。
        TimestampWithTZ = 20,
        //
        // 摘要:
        //     Oracle VARCHAR2 数据类型，它包含最大为 4,000 字节的变长字符串。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.String 或 OracleClient System.Data.OracleClient.OracleString
        //     数据类型。
        VarChar = 22,
        //
        // 摘要:
        //     An integral type representing unsigned 8-bit integers with values between
        //     0 and 255.这不是本机的 Oracle 数据类型，但是提供此类型以提高绑定输入参数时的性能。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.Byte 数据类型。
        Byte = 23,
        //
        // 摘要:
        //     An integral type representing unsigned 16-bit integers with values between
        //     0 and 65535.这不是本机的 Oracle 数据类型，但是提供此类型以提高绑定输入参数时的性能。有关从 Oracle 数值转换为公共语言运行时
        //     (CLR) 数据类型的信息，请参见 System.Data.OracleClient.OracleNumber。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.UInt16 或 OracleClient System.Data.OracleClient.OracleNumber
        //     数据类型。
        UInt16 = 24,
        //
        // 摘要:
        //     An integral type representing unsigned 32-bit integers with values between
        //     0 and 4294967295.这不是本机的 Oracle 数据类型，但是提供此类型以提高绑定输入参数时的性能。有关从 Oracle 数值转换为公共语言运行时
        //     (CLR) 数据类型的信息，请参见 System.Data.OracleClient.OracleNumber。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.UInt32 或 OracleClient System.Data.OracleClient.OracleNumber
        //     数据类型。
        UInt32 = 25,
        //
        // 摘要:
        //     An integral type representing signed 8 bit integers with values between -128
        //     and 127.这不是本机的 Oracle 数据类型，但是提供此类型以提高绑定输入参数时的性能。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.SByte 数据类型。
        SByte = 26,
        //
        // 摘要:
        //     An integral type representing signed 16-bit integers with values between
        //     -32768 and 32767.这不是本机的 Oracle 数据类型，但是提供此类型以提高绑定输入参数时的性能。有关从 Oracle 数值转换为公共语言运行时
        //     (CLR) 数据类型的信息，请参见 System.Data.OracleClient.OracleNumber。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.Int16 或 OracleClient System.Data.OracleClient.OracleNumber
        //     数据类型。
        Int16 = 27,
        //
        // 摘要:
        //     An integral type representing signed 32-bit integers with values between
        //     -2147483648 and 2147483647.This is not a native Oracle data type, but is
        //     provided for performance when binding input parameters.有关从 Oracle 数值转换到公共语言运行时数据类型的信息，请参见
        //     System.Data.OracleClient.OracleNumber。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.Int32 或 OracleClient System.Data.OracleClient.OracleNumber
        //     数据类型。
        Int32 = 28,
        //
        // 摘要:
        //     单精度浮点值。这不是本机的 Oracle 数据类型，但是提供此类型以提高绑定输入参数时的性能。有关从 Oracle 数值转换到公共语言运行时数据类型的信息，请参见
        //     System.Data.OracleClient.OracleNumber。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.Single 或 OracleClient System.Data.OracleClient.OracleNumber
        //     数据类型。
        Float = 29,
        //
        // 摘要:
        //     一个双精度浮点值。这不是本机的 Oracle 数据类型，但是提供此类型以提高绑定输入参数时的性能。有关从 Oracle 数值转换为公共语言运行时
        //     (CLR) 数据类型的信息，请参见 System.Data.OracleClient.OracleNumber。在 System.Data.OracleClient.OracleParameter.Value
        //     中使用 .NET Framework System.Double 或 OracleClient System.Data.OracleClient.OracleNumber
        //     数据类型。
        Double = 30,
    }

    /// <summary>
    /// 用于 Vic.Data.DataAccess 中 Parameter 参数 OracleClient、OracleManaged 特定的数据类型
    /// </summary>
    [SuppressMessage("Rule Category", "CS1591")]
    [Serializable]
    public enum OracleClientType
    {
        //BFile = 101,
        Blob = 102,
        Byte = 103,
        Char = 104,
        Clob = 105,
        Date = 106,
        Decimal = 107,
        Double = 108,
        Long = 109,
        //LongRaw = 110,
        Int16 = 111,
        Int32 = 112,
        Int64 = 113,
        //IntervalDS = 114,
        //IntervalYM = 115,
        NClob = 116,
        NChar = 117,
        NVarchar2 = 119,
        //Raw = 120,
        RefCursor = 121,
        //Single = 122,
        TimeStamp = 123,
        //TimeStampLTZ = 124,
        //TimeStampTZ = 125,
        Varchar2 = 126,
        XmlType = 127,
        //Array = 128,
        //Object = 129,
        //Ref = 130,
        //BinaryDouble = 132,
        //BinaryFloat = 133,
    }
    #endregion
}
