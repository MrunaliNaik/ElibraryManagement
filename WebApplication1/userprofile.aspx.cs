using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class userprofile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"].ToString() == "" || Session["username"] == null)
                {
                    Response.Write("<script>alert('Session Expired Login Again');</script>");
                    Response.Redirect("userlogin.aspx");
                }
                else
                {
                    getUserBookData();

                    if (!Page.IsPostBack)
                    {
                        getUserPersonalDetails();
                    }

                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("userlogin.aspx");
            }
        }

        // update button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("userlogin.aspx");
            }
            else
            {
                updateUserPersonalDetails();

            }
        }



        // user defined function


        void updateUserPersonalDetails()
        {
            string password = "";
            if (TextBox10.Text.Trim() == "")
            {
                password = TextBox9.Text.Trim();
            }
            else
            {
                password = TextBox10.Text.Trim();
            }
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("update member_master_tbl set full_name=@full_name, dob=@dob, contact_no=@contact_no, email=@email, state=@state, city=@city, pincode=@pincode, full_address=@full_address, password=@password, account_status=@account_status WHERE member_id='" + Session["username"].ToString().Trim() + "'", con);

                cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@account_status", "pending");

                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {

                    Response.Write("<script>alert('Your Details Updated Successfully');</script>");
                    getUserPersonalDetails();
                    getUserBookData();
                }
                else
                {
                    Response.Write("<script>alert('Invaid entry');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        void getUserPersonalDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from member_master_tbl where member_id='" + Session["username"].ToString() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                TextBox1.Text = dt.Rows[0]["full_name"].ToString();
                TextBox2.Text = dt.Rows[0]["dob"].ToString();
                TextBox3.Text = dt.Rows[0]["contact_no"].ToString();
                TextBox4.Text = dt.Rows[0]["email"].ToString();
                DropDownList1.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
                TextBox6.Text = dt.Rows[0]["city"].ToString();
                TextBox7.Text = dt.Rows[0]["pincode"].ToString();
                TextBox5.Text = dt.Rows[0]["full_address"].ToString();
                TextBox8.Text = dt.Rows[0]["member_id"].ToString();
                TextBox9.Text = dt.Rows[0]["password"].ToString();

                Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();

                if (dt.Rows[0]["account_status"].ToString().Trim() == "active")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-success");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "pending")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-warning");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "deactive")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-danger");
                }
                else
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-info");
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        void getUserBookData()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from book_issue_tbl where member_id='" + Session["username"].ToString() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Check your condition here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}






/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class userprofile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"].ToString() == "" || Session["username"] == null)
                {
                    Response.Write("<script>alert('Session Expired Login Again');</script>");
                    Response.Redirect("userlogin.aspx");
                }
                else
                {
                    getUserBookData();

                    if (!Page.IsPostBack)
                    {
                        getUserPersonalDetails();
                    }

                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("userlogin.aspx");
            }
        }

        //update
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("userlogin.aspx");
            }
            else
            {
                updateUserPersonalDetails();

            }
        }

        //user defined

        void updateUserPersonalDetails()
        {
            string password = "";
            if (TextBox10.Text.Trim() == "")
            {
                password = TextBox9.Text.Trim();
            }
            else
            {
                password = TextBox10.Text.Trim();
            }
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("update member_master_tbl set full_name=@full_name, dob=@dob, contact_no=@contact_no, email=@email, state=@state, city=@city, pincode=@pincode, full_address=@full_address, password=@password, account_status=@account_status WHERE member_id='" + Session["username"].ToString().Trim() + "'", con);

                cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@account_status", "pending");

                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {

                    Response.Write("<script>alert('Your Details Updated Successfully');</script>");
                    getUserPersonalDetails();
                    getUserBookData();
                }
                else
                {
                    Response.Write("<script>alert('Invaid entry');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        void getUserPersonalDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from member_master_tbl where member_id='" + Session["username"].ToString() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                TextBox1.Text = dt.Rows[0]["full_name"].ToString();
                TextBox2.Text = dt.Rows[0]["dob"].ToString();
                TextBox3.Text = dt.Rows[0]["contact_no"].ToString();
                TextBox4.Text = dt.Rows[0]["email"].ToString();
                DropDownList1.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
                TextBox6.Text = dt.Rows[0]["city"].ToString();
                TextBox7.Text = dt.Rows[0]["pincode"].ToString();
                TextBox5.Text = dt.Rows[0]["full_address"].ToString();
                TextBox8.Text = dt.Rows[0]["member_id"].ToString();
                TextBox9.Text = dt.Rows[0]["password"].ToString();

                Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();

                if (dt.Rows[0]["account_status"].ToString().Trim() == "active")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-success");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "pending")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-warning");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "deactive")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-danger");
                }
                else
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-info");
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }


            void getUserBookData()
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * from book_issue_table where member_id='" + Session["username"].ToString() + "';", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");

                }
            }

            protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                try
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Check your condition here
                        DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                        DateTime today = DateTime.Today;
                        if (today > dt)
                        {
                            e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }


        }

    }
}




***************<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
< asp:Content ID = "Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class= "container-fluid" >
      < div class= "row" >
 
          < div class= "col-md-5" >
  
              < div class= "card" >
   
                  < div class= "card-body" >
    
                      < div class= "row" >
     
                          < div class= "col" >
      
                              < center >
      
                                 < img width = "100px" src = "imgs/generaluser.png" />
         
                                 </ center >
         
                              </ div >
         
                           </ div >
         
                           < div class= "row" >
          
                               < div class= "col" >
           
                                   < center >
           
                                      < h4 > Your Profile </ h4 >
              
                                         < span > Account Status - </ span >
                 
                                            < asp:Label class= "badge badge-pill badge-info" ID = "Label1" runat = "server" Text = "Your status" ></ asp:Label >
                           
                                                   </ center >
                           
                                                </ div >
                           
                                             </ div >
                           
                                             < div class= "row" >
                            
                                                 < div class= "col" >
                             
                                                     < hr >
                             
                                                  </ div >
                             
                                               </ div >
                             
                                               < div class= "row" >
                              
                                                   < div class= "col-md-6" >
                               
                                                       < label > Full Name </ label >
                                  
                                                          < div class= "form-group" >
                                   
                                                              < asp:TextBox CssClass = "form-control" ID="TextBox1" runat="server" placeholder="Full Name"></asp:TextBox >
                                      
                                                              </ div >
                                      
                                                           </ div >
                                      
                                                           < div class= "col-md-6" >
                                       
                                                               < label > Date of Birth</label>
                                                                  <div class= "form-group" >
                                          
                                                                     < asp:TextBox CssClass = "form-control" ID="TextBox2" runat="server" placeholder="Password" TextMode="Date"></asp:TextBox >
                                             
                                                                     </ div >
                                             
                                                                  </ div >
                                             
                                                               </ div >
                                             
                                                               < div class= "row" >
                                              
                                                                   < div class= "col-md-6" >
                                               
                                                                       < label > Contact No </ label >
                                                  
                                                                          < div class= "form-group" >
                                                   
                                                                              < asp:TextBox CssClass = "form-control" ID="TextBox3" runat="server" placeholder="Contact No" TextMode="Number"></asp:TextBox >
                                                      
                                                                              </ div >
                                                      
                                                                           </ div >
                                                      
                                                                           < div class= "col-md-6" >
                                                       
                                                                               < label > Email ID </ label >
                                                          
                                                                                  < div class= "form-group" >
                                                           
                                                                                      < asp:TextBox CssClass = "form-control" ID="TextBox4" runat="server" placeholder="Email ID" TextMode="Email"></asp:TextBox >
                                                              
                                                                                      </ div >
                                                              
                                                                                   </ div >
                                                              
                                                                                </ div >
                                                              
                                                                                < div class= "row" >
                                                               
                                                                                    < div class= "col-md-4" >
                                                                
                                                                                        < label > State </ label >
                                                                
                                                                                        < div class= "form-group" >
                                                                 
                                                                                            < asp:DropDownList class= "form-control" ID = "DropDownList1" runat = "server" >
                                                                     
                                                                                                   < asp:ListItem Text = "Select" Value="select" />
                              <asp:ListItem Text = "Andhra Pradesh" Value="Andhra Pradesh" />
                              <asp:ListItem Text = "Arunachal Pradesh" Value="Arunachal Pradesh" />
                              <asp:ListItem Text = "Assam" Value="Assam" />
                              <asp:ListItem Text = "Bihar" Value="Bihar" />
                              <asp:ListItem Text = "Chhattisgarh" Value="Chhattisgarh" />
                              <asp:ListItem Text = "Rajasthan" Value="Rajasthan" />
                              <asp:ListItem Text = "Goa" Value="Goa" />
                              <asp:ListItem Text = "Gujarat" Value="Gujarat" />
                              <asp:ListItem Text = "Haryana" Value="Haryana" />
                              <asp:ListItem Text = "Himachal Pradesh" Value="Himachal Pradesh" />
                              <asp:ListItem Text = "Jammu and Kashmir" Value="Jammu and Kashmir" />
                              <asp:ListItem Text = "Jharkhand" Value="Jharkhand" />
                              <asp:ListItem Text = "Karnataka" Value="Karnataka" />
                              <asp:ListItem Text = "Kerala" Value="Kerala" />
                              <asp:ListItem Text = "Madhya Pradesh" Value="Madhya Pradesh" />
                              <asp:ListItem Text = "Maharashtra" Value="Maharashtra" />
                              <asp:ListItem Text = "Manipur" Value="Manipur" />
                              <asp:ListItem Text = "Meghalaya" Value="Meghalaya" />
                              <asp:ListItem Text = "Mizoram" Value="Mizoram" />
                              <asp:ListItem Text = "Nagaland" Value="Nagaland" />
                              <asp:ListItem Text = "Odisha" Value="Odisha" />
                              <asp:ListItem Text = "Punjab" Value="Punjab" />
                              <asp:ListItem Text = "Rajasthan" Value="Rajasthan" />
                              <asp:ListItem Text = "Sikkim" Value="Sikkim" />
                              <asp:ListItem Text = "Tamil Nadu" Value="Tamil Nadu" />
                              <asp:ListItem Text = "Telangana" Value="Telangana" />
                              <asp:ListItem Text = "Tripura" Value="Tripura" />
                              <asp:ListItem Text = "Uttar Pradesh" Value="Uttar Pradesh" />
                              <asp:ListItem Text = "Uttarakhand" Value="Uttarakhand" />
                              <asp:ListItem Text = "West Bengal" Value="West Bengal" />
                           </asp:DropDownList >
                        </ div >
                     </ div >
                     < div class= "col-md-4" >
 
                         < label > City </ label >
 
                         < div class= "form-group" >
  
                             < asp:TextBox class= "form-control" ID = "TextBox6" runat = "server" placeholder = "City" ></ asp:TextBox >
            
                                    </ div >
            
                                 </ div >
            
                                 < div class= "col-md-4" >
             
                                     < label > Pincode </ label >
             
                                     < div class= "form-group" >
              
                                         < asp:TextBox class= "form-control" ID = "TextBox7" runat = "server" placeholder = "Pincode" TextMode = "Number" ></ asp:TextBox >
                          
                                                  </ div >
                          
                                               </ div >
                          
                                            </ div >
                          
                                            < div class= "row" >
                           
                                                < div class= "col" >
                            
                                                    < label > Full Address </ label >
                               
                                                       < div class= "form-group" >
                                
                                                           < asp:TextBox CssClass = "form-control" ID="TextBox5" runat="server" placeholder="Full Address" TextMode="MultiLine" Rows="2"></asp:TextBox >
                                   
                                                           </ div >
                                   
                                                        </ div >
                                   
                                                     </ div >
                                   
                                                     < div class= "row" >
                                    
                                                         < div class= "col" >
                                     
                                                             < center >
                                     
                                                                < span class= "badge badge-pill badge-info" > Login Credentials </ span >
                                         
                                                                 </ center >
                                         
                                                              </ div >
                                         
                                                           </ div >
                                         
                                                           < div class= "row" >
                                          
                                                               < div class= "col-md-4" >
                                           
                                                                   < label > User ID </ label >
                                              
                                                                      < div class= "form-group" >
                                               
                                                                          < asp:TextBox class= "form-control" ID = "TextBox8" runat = "server" placeholder = "User ID" ReadOnly = "True" ></ asp:TextBox >
                                                           
                                                                                   </ div >
                                                           
                                                                                </ div >
                                                           
                                                                                < div class= "col-md-4" >
                                                            
                                                                                    < label > Old Password </ label >
                                                               
                                                                                       < div class= "form-group" >
                                                                
                                                                                           < asp:TextBox class= "form-control" ID = "TextBox9" runat = "server" placeholder = "Email ID" TextMode = "Password" ReadOnly = "True" ></ asp:TextBox >
                                                                              
                                                                                                      </ div >
                                                                              
                                                                                                   </ div >
                                                                              
                                                                                                   < div class= "col-md-4" >
                                                                               
                                                                                                       < label > New Password </ label >
                                                                                  
                                                                                                          < div class= "form-group" >
                                                                                   
                                                                                                              < asp:TextBox class= "form-control" ID = "TextBox10" runat = "server" placeholder = "Email ID" TextMode = "Password" ></ asp:TextBox >
                                                                                               
                                                                                                                       </ div >
                                                                                               
                                                                                                                    </ div >
                                                                                               
                                                                                                                 </ div >
                                                                                               
                                                                                                                 < div class= "row" >
                                                                                                
                                                                                                                     < div class= "col-8 mx-auto" >
                                                                                                 
                                                                                                                         < center >
                                                                                                 
                                                                                                                            < div class= "form-group" >
                                                                                                  
                                                                                                                                < asp:Button class= "btn btn-primary btn-block btn-lg" ID = "Button1" runat = "server" Text = "Update" OnClick = "Button1_Click" />
                                                                                                          
                                                                                                                                     </ div >
                                                                                                          
                                                                                                                                  </ center >
                                                                                                          
                                                                                                                               </ div >
                                                                                                          
                                                                                                                            </ div >
                                                                                                          
                                                                                                                         </ div >
                                                                                                          
                                                                                                                      </ div >
                                                                                                          
                                                                                                                      < a href = "homepage.aspx" ><< Back to Home</a><br><br>
         </div>
         <div class= "col-md-7" >
            < div class= "card" >
 
                < div class= "card-body" >
  
                    < div class= "row" >
   
                        < div class= "col" >
    
                            < center >
    
                               < img width = "100px" src = "imgs/books1.png" />
       
                               </ center >
       
                            </ div >
       
                         </ div >
       
                         < div class= "row" >
        
                             < div class= "col" >
         
                                 < center >
         
                                    < h4 > Your Issued Books</h4>
                                       <asp:Label class= "badge badge-pill badge-info" ID = "Label2" runat = "server" Text = "your books info" ></ asp:Label >
                     
                                             </ center >
                     
                                          </ div >
                     
                                       </ div >
                     
                                       < div class= "row" >
                      
                                           < div class= "col" >
                       
                                               < hr >
                       
                                            </ div >
                       
                                         </ div >
                       
                                         < div class= "row" >
                        
                                             < div class= "col" >
                         
                                                 < asp:GridView class= "table table-striped table-bordered" ID = "GridView1" runat = "server" ></ asp:GridView >
                                 
                                                      </ div >
                                 
                                                   </ div >
                                 
                                                </ div >
                                 
                                             </ div >
                                 
                                          </ div >
                                 
                                       </ div >
                                 
                                    </ div >
                                 </ asp:Content >
*/