using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace APILogicLib
{
	// 암호 난이도가 높지 않으니 가급적 사용을 자제합시다.

	// 출처: http://minamjun11.egloos.com/780264

	// 사용예
	//string JSonData = "fdfdfd";

	//string secretKey = "secretky";
	//string EncryptString = _3DESEncrypt.Encrypt(JSonData, secretKey);

	//string DeEncryptString = _3DESEncrypt.Decrypt(EncryptString, secretKey);



	class _3DESEncrypt
	{
		public static string Encrypt(string Message, string Passphrase)   
		{         
			byte[] Results;      
			System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

			// Step 1. MD5 해쉬를 사용해서 암호화하고,	   
			// MD5 해쉬 생성기를 사용해서 결과는 128 비트 바이트 배열인데,          
			// 3DES 인코딩을 위한 올바른 길이가 됨.

			MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();            
			byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));            

			// Step 2. TripleDESCryptoServiceProvider object 생성            
			TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

			// Step 3. Encoder 설정
			TDESAlgorithm.Key = TDESKey;            
			TDESAlgorithm.Mode = CipherMode.ECB;            
			TDESAlgorithm.Padding = PaddingMode.PKCS7;

			// Step 4. 암호화할 문자열을 Byte[]로 변환          
			byte[] DataToEncrypt = UTF8.GetBytes(Message);         

			// Step 5. 실제로 문자열을 암호화      
			try        
			{      
				ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
				Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length); 
			}
			finally      
			{
				// 중요한 3DES, Hashprovider의 속성을 해제       
				TDESAlgorithm.Clear();        
				HashProvider.Clear();     
			}
		
				// Step 6. 암호화된 문자열을 Base64로 변환하여 리턴      
			return Convert.ToBase64String(Results);
		}     


		public static string Decrypt(string Message, string Passphrase)    
		{  
			byte[] Results;          
			System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();        
			MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();       
			byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

			// Step 2. TripleDESCryptoServiceProvider object 생성       
			TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

			// Step 3. Decoder 설정
			TDESAlgorithm.Key = TDESKey;       
			TDESAlgorithm.Mode = CipherMode.ECB;       
			TDESAlgorithm.Padding = PaddingMode.PKCS7;       

			// Step 4. 인자로 받은 문자열을 Byte[]로 변환       
			byte[] DataToDecrypt = Convert.FromBase64String(Message);

			// Step 5. 실제 문자열 복호화
			try         
			{
				ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
				Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
			}     
			finally
			{          
				// 중요한 3DES, Hashprovider의 속성을 해제
				TDESAlgorithm.Clear();                
				HashProvider.Clear();            
			}

			// Step 6. UTF-8 형태로 복호화된 문자열 리턴 
			return UTF8.GetString( Results );     
		}

	}
}
