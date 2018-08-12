using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    public void TestA(){
         //log.Debug(string.Format("Uploading {0} to {1}", file, url));
        //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");

        string boundary = String.Format("----------{0:N}", Guid.NewGuid());


        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        // Create a request using a URL that can receive a post.   
        WebRequest request = WebRequest.Create("http://localhost/test/uno.php");
        // Set the Method property of the request to POST.  
        request.Method = "POST";
        // Set the ContentType property of the WebRequest.  
        //request.ContentType = ContentType = "application/x-www-form-urlencoded";
        request.ContentType = ContentType = "multipart/form-data; boundary=" + boundary;

        //request.KeepAlive = true;
        request.Credentials = System.Net.CredentialCache.DefaultCredentials;

       





        // Read file data
        FileStream fs = new FileStream("C:\\Users\\user\\Documents\\test.txt", FileMode.Open, FileAccess.Read);
        byte[] fileU = new byte[fs.Length];
        fs.Read(fileU,0 , fileU.Length);
        fs.Close();
        String parametro = "file";
        String filename = "test.txt";
        Stream dataStream = request.GetRequestStream();

        string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                    boundary,
                    parametro,
                    filename,
                    "application/octet-stream");

         dataStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
         dataStream.Write(fileU, 0, fileU.Length);
        

        /* string postData = string.Format("--{0}\r\nContent-Disposition: form-data; nombre=\"{1}\"\r\n\r\n{2}",
                     boundary,
                     "nombre",
                     "xxxx");

         //dataStream.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));
        */

        // Create OST data and convert it to a byte array.  
        // String postData = string.Format("nombre=\"{0}\"",
         //             "nombre");


         string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    "nombre",
                    "punto");
       Encoding.UTF8.GetBytes(postData);
       //request.ContentLength = Encoding.UTF8.GetBytes(postData).Length;
      
       dataStream.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));
     
        // Set the ContentLength property of the WebRequest.  

      
        // Get the request stream.  
        




        // Write the data to the request stream.  
       // dataStream.Write(byteArray, 0, byteArray.Length);
        // Close the Stream object.  


        dataStream.Close();
        // Get the response.  
        WebResponse response = request.GetResponse();
        // Display the status.  
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.  
        dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.  
        StreamReader reader = new StreamReader(dataStream);
        // Read the content.  
        string responseFromServer = reader.ReadToEnd();
        // Display the content.  
        Console.WriteLine(responseFromServer);
        // Clean up the streams.  
        dataStream.Close();
   
        response.Close();  
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
         //log.Debug(string.Format("Uploading {0} to {1}", file, url));
        //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");

        

        //String filePath = FileUpload1.FileContent;

        if (FileUpload1!=null)
        {
            string boundary = String.Format("----------{0:N}", Guid.NewGuid());

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/test/dos.php");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Set the ContentType property of the WebRequest.  
            //request.ContentType = ContentType = "application/x-www-form-urlencoded";
            request.ContentType = ContentType = "multipart/form-data; boundary=" + boundary;

            //request.KeepAlive = true;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;

            // Read file data
           /* FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] fileU = new byte[fs.Length];
            fs.Read(fileU, 0, fileU.Length);
            fs.Close();
            */

            byte[] fileU = new byte[FileUpload1.PostedFile.ContentLength - 1];
            fileU = FileUpload1.FileBytes;






            String parametro = "file";
            String filename = FileUpload1.FileName;
            Stream dataStream = request.GetRequestStream();

            string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        parametro,
                        filename,
                        "application/octet-stream");

            dataStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
            dataStream.Write(fileU, 0, fileU.Length);





            String postData = string.Format("\r\n--{0}" +
                                    "\r\nContent-Disposition: form-data; name=\"{1}\";\r\n\r\n{2}",
                                    boundary,
                                    "nombre",
                                    TextBox1.Text);
            dataStream.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));

            dataStream.Close();

            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            Console.WriteLine(responseFromServer);
            // Clean up the streams.  
            dataStream.Close();

            response.Close();
            Label1.Text = "dddddddddddddddddddddd";
            string mensaje = "<script type='text/javascript'>alert('{0}')</script>";

            mensaje = string.Format(mensaje, msg);
            ScriptManager.RegisterStartupScript(this, this.GetType, "msgBox", mensaje, true);
        }
       
    }
}
