using SwaggerPetstoreOpenAPI30.Standard.Controllers;
using SwaggerPetstoreOpenAPI30.Standard.Exceptions;
using SwaggerPetstoreOpenAPI30.Standard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SwaggerPetstoreOpenAPI30.Standard.SwaggerPetstoreOpenAPI30Client client;
        UserController userController;
        public Form1()
        {
            InitializeComponent();
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //откуда код брал
            //https://github.com/sdks-io/swagger-petstore-3-dotnet-sdk/blob/1.0.0/doc/controllers/user.md
            //https://www.nuget.org/packages/sdksio.SwaggerPetstore3SDK/1.0.0?_src=template
            //https://petstore.swagger.io/#/ 

            var client = new SwaggerPetstoreOpenAPI30.Standard.SwaggerPetstoreOpenAPI30Client.Builder()
            // регал этот ключ на https://petstore.swagger.io/#/ 
    .Build();


            long? id = 10L;
            string username = "dsa";
            string firstName = "John5";
            string lastName = "James5";
            string email = "john5@email.com";
            string password = "123455";
            string phone = "123455";
            int? userStatus = 15;

            client.HttpClientConfiguration.HttpClientInstance.BaseAddress = new Uri("https://petstore.swagger.io/v2/swagger.json"); ;
            UserController userController = client.UserController;

            try
            {
                User result = await userController.CreateUserAsync(id, username, firstName, lastName, email, password, phone, userStatus);
            }
            catch (ApiException e1)
            {
                // TODO: Handle exception here
                MessageBox.Show(e1.Message); //HTTP Response Not OK 
            }
        }
    }
}
