<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatchDoctor.aspx.cs" Inherits="SitefinityWebApp.PatchDoctor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div>
            <asp:Label runat="server" ID="lblUser" Text="No User"/>  
            <hr />
            <br />
            <asp:Label runat="server" ID="lblDoctor" Text="No Doctor Loaded"/>  
            <hr />
            <br />  
           <%-- <asp:dropdownlist id="ShowDataDropDown" runat="server" autopostback="true" datatextfield="Text"
                datavaluefield="Value" onselectedindexchanged="Show_All_From_Drop_Down">
            </asp:dropdownlist>
            <br />
            <br />--%>
            <asp:Button Text="Patch Specialty" runat="server" OnClick="Patch_Specialty" />  
            <br />
            <br />
            <asp:Button Text="Patch Specialty Pantai" runat="server" OnClick="Patch_Specialty_Pantai" />  
            <br />
            <br />
            <asp:Button Text="Patch Doctor" runat="server" OnClick="Patch_Doctor_2" />  
            <br />
            <br />
            <asp:Button Text="Patch Doctor Pantai" runat="server" OnClick="Patch_Doctor_Pantai_2" />  
            <br />
            <br />
            <asp:Button Text="Patch Doctor PCMC" runat="server" OnClick="Patch_Doctor_PCMC" /> 
            <br />
            <br />
            <asp:Button Text="Patch GE Doctor Image" runat="server" OnClick="Patch_Doctor_Image_GE" />  
            <br />
            <br />
            <asp:Button Text="Patch Pantai Doctor Image" runat="server" OnClick="Patch_Doctor_Image_Pantai" />  
            <br />
            <br />
            <asp:Button Text="Patch PCMC Doctor Image" runat="server" OnClick="Patch_Doctor_Image_PCMC" /> 
            <%--<asp:Button Text="Patch Clinic Hour" runat="server" OnClick="Patch_Clinic_Hour" />  
            <br />
            <br />
            <asp:Button Text="Patch OPD Day" runat="server" OnClick="Patch_OPD_Day" />  
            <br />
            <br />
            <asp:Button Text="Remove Duplicated Doctor" runat="server" OnClick="Delete_Duplicated" />    
            <br />
            <br />
            <asp:Button Text="Delete Clinic Hour" runat="server" OnClick="Delete_Clinic_Hour" />  
            <br />
            <br />--%>
            <br />
            <hr />
            <br />
            <asp:Button Text="Dummy Data" runat="server" OnClick="Patch_Doctor_Dummy" />  
            <br />
            <br />
            <asp:Button Text="Remove Empty Doctors" runat="server" OnClick="Remove_Empty_Doctor" />  
          <%--  <br />
            <asp:Button Text="Patch Details" runat="server" OnClick="Patch_Specialty_Detail" />            
            <br />
            <asp:Button Text="Patch Accodion" runat="server" OnClick="Patch_Specialty_Accodions" />--%>
        </div>
    </form>
</body>
</html>
