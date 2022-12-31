using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using OpenAI_API;

namespace ChatbotClientCSharp
{
    public partial class ChatbotForm : Form
    {
        // Declare the controls
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;

        public ChatbotForm()
        {
            InitializeComponent();
            // Create the Button
            button1 = new Button();
            button1.Location = new System.Drawing.Point(12, 68);
            button1.Size = new System.Drawing.Size(360, 30);
            button1.Text = "Ask Anything";
            button1.Click += new EventHandler(Button1_Click);

            // Create the input TextBox
            textBox1 = new TextBox();
            textBox1.Location = new System.Drawing.Point(12, 12);
            textBox1.Size = new System.Drawing.Size(360, 110);
            textBox1.Multiline = true;
            textBox1.Lines = new[] { "5" };
            textBox1.Text = "Enter your question here";
            
            // Create the output TextBox
            textBox2 = new TextBox();
            textBox2.Location = new System.Drawing.Point(12, 128);
            textBox2.Size = new System.Drawing.Size(360, 440);
            textBox2.Multiline = true;
            textBox2.Lines = new[] { "20" };
            textBox2.ReadOnly = true;
            textBox2.Text = "Answer will appear here";

          

            // Add the controls to the Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChatbotForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Name = "ChatbotForm";
            this.ResumeLayout(false);
        }
        private async void Button1_Click(object sender, EventArgs e)
        {
            // Get the question from the input TextBox
            string question = textBox1.Text;

            // Do something with the question (e.g., send it to a question answering service)
            // string answer = "answered"; // GetAnswer(question);
            // Set the model to use
            string modelEngine = "text-davinci-003";

            // Set the prompt for the model
            string answer = textBox1.Text;

            string response = await ChatWithOpenAI(modelEngine, answer);


            // Display the answer in the output TextBox
            textBox2.Text = response;
        }
        private static async Task<string> ChatWithOpenAI(string modelEngine, string prompt)
        {
            // Set your API key
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            // Build the request URL
            string requestUrl = $"https://api.openai.com/v1/completions";

            // Build the request body
            string requestBody = JsonConvert.SerializeObject(new
            {
                model = modelEngine,
                prompt,
                max_tokens = 1024,
                n = 1,
                stop = (string)null,
                temperature = 0.8
            });

            // Create the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = "POST";
            request.Headers["Authorization"] = $"Bearer {apiKey}";
            request.ContentType = "application/json";

            // Write the request body
            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestBody);
                streamWriter.Flush();
                streamWriter.Close();
            }

            // Send the request and get the response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Read the response
            string responseText;
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseText = streamReader.ReadToEnd();
            }

            // Deserialize the response
            dynamic responseJson = JsonConvert.DeserializeObject(responseText);

            // Extract the response text
            return responseJson.choices[0].text.ToString();
        }
    }
}