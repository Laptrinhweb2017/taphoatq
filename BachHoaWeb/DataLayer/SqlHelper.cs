using Application.Asset.Dtos;
using Newtonsoft.Json;
using TextProcessing;

namespace ModelLib
{
	public class SqlHelper
	{
		public static string SqlConn()
		{
			Encryption encryption = new();
			string conf = File.ReadAllText("conf.cfg");
			conf = encryption.Decrypt(conf, "truonghoai");
			DtoConf confobj = JsonConvert.DeserializeObject<DtoConf>(conf)!;

			return $"Server={confobj.ServerIP};Database=AttendanceDB;Encrypt=false;User ID={confobj.UserID};Password={confobj.Pass}";
		}
	}
}