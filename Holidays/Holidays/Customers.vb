Imports System.IO

Public Class frmCustomers

    ' Structure for the customer's information
    Private Structure Customer

        Public CustomerID As String ' Used to uniquely identify the customer
        Public FirstName As String
        Public Surname As String
        Public EmailAddress As String
        Public PhoneNumber As String

    End Structure

    Private Sub Customers_Load() Handles MyBase.Load

        ' If there is not a file with this name already
        If Dir$("customerdetails.txt") = "" Then

            ' Create the file
            Dim sw As New StreamWriter("customerdetails.txt", True)

            ' Write '0' to the file
            sw.WriteLine("0")

            ' File needs to be closed after it is used
            sw.Close()

            ' Output that a new file has been created
            MsgBox("A new file has been created", vbExclamation, "Warning!")

        End If

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        ' New customer data is being saved
        Dim CustomerData As New Customer

        Dim CustomersData() As String = File.ReadAllLines(Dir$("customerdetails.txt"))

        ' Where the data will be saved
        Dim sw As New System.IO.StreamWriter("customerdetails.txt", True)

        ' Call the function to generate the Customer ID
        GenerateID(CustomersData)

        ' If the validation checks have been passed
        If Validation() = True Then

            ' The data from the textboxes is padded out then saved to the structure
            CustomerData.CustomerID = LSet(txtCustomerID.Text, 4)
            CustomerData.FirstName = LSet(txtFirstName.Text, 20)
            CustomerData.Surname = LSet(txtSurname.Text, 20)
            CustomerData.EmailAddress = LSet(txtEmailAddress.Text, 30)
            CustomerData.PhoneNumber = LSet(txtPhoneNumber.Text, 11)

            ' The data is saved to the textfile
            sw.WriteLine(CustomerData.CustomerID & CustomerData.FirstName & CustomerData.Surname & CustomerData.EmailAddress & CustomerData.PhoneNumber)

            ' File needs to be closed after it is used
            sw.Close()

            ' Output that the file has been saved
            MsgBox("File Saved!")

            ClearTextboxes()

        End If

    End Sub

    Private Sub cmdCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCount.Click

        ' Variable to store the number of customers found
        Dim CustomersFound As Integer
        CustomersFound = 0

        Dim CustomersData() As String = File.ReadAllLines("customerdetails.txt")

        ' If the user is not searching a Customer ID
        If txtCustomerID.Text = "" Then

            ' Start at the beginning of the data until the last set of data
            For i = 0 To UBound(CustomersData)

                ' If the data in the textfile is the same as the data in a textbox
                If ((Trim(Mid(CustomersData(i), 5, 20)) = txtFirstName.Text) Or txtFirstName.Text = "") And ((Trim(Mid(CustomersData(i), 25, 20)) = txtSurname.Text) Or txtSurname.Text = "") And ((Trim(Mid(CustomersData(i), 45, 30)) = txtEmailAddress.Text) Or txtEmailAddress.Text = "") And ((Trim(Mid(CustomersData(i), 75, 11)) = txtPhoneNumber.Text) Or txtPhoneNumber.Text = "") Then

                    ' A customer has been found
                    CustomersFound = CustomersFound + 1

                End If

            Next

            ' If more than 1 customer has been found
            If CustomersFound > 0 Then

                ' Output the number of customers found
                MsgBox(CustomersFound & " customers were found.")

            Else

                ' Otherwise output that no customers were found
                MsgBox("No customers were found.")

            End If

        End If

        ' If the user is searching a Customer ID
        If Not txtCustomerID.Text = "" Then

            For i = 0 To UBound(CustomersData)

                ' If there is a match for the Customer ID
                If Trim(Mid(CustomersData(i), 1, 4)) = txtCustomerID.Text Then

                    ' Ouput the data for the Customer ID has been found
                    MsgBox("A customer with this Customer ID has been found.")

                    ' Output the data to the textboxes
                    txtFirstName.Text = (Trim(Mid(CustomersData(i), 5, 20)))
                    txtSurname.Text = (Trim(Mid(CustomersData(i), 25, 20)))
                    txtEmailAddress.Text = (Trim(Mid(CustomersData(i), 45, 30)))
                    txtPhoneNumber.Text = (Trim(Mid(CustomersData(i), 75, 11)))

                End If

            Next

        End If

    End Sub

    Public Function Validation()

        If txtFirstName.Text = "" Or txtSurname.Text = "" Or txtEmailAddress.Text = "" Or txtPhoneNumber.Text = "" Then

            ' Presence check
            MsgBox("You must enter something for Name, Email Address and Phone Number.")

            Return False

        End If

        If (txtFirstName.Text).Length > 20 Or (txtSurname.Text).Length > 20 Then

            ' Length check
            MsgBox("First Name and Surname must both be less than 20 characters long.")

            Return False

        ElseIf (txtEmailAddress.Text).Contains("@") = False Or (txtEmailAddress.Text).Contains(".") = False Then

            ' Format check
            MsgBox("Email Address must contain an '@' and a dot.")

            Return False

        ElseIf IsNumeric(txtPhoneNumber.Text) = False Then

            ' Type check
            MsgBox("Phone Number must be numeric.")

            Return False

        Else

            Return True

        End If

    End Function

    Public Sub GenerateID(CustomersData)

        ' Highest current Customer ID
        Dim CurrentCustomerID As Integer

        ' Set to zero by default
        CurrentCustomerID = 0

        For i = 0 To UBound(CustomersData)

            ' If the highest ID in the textfile is equal to the current highest ID
            If Val(Trim(Mid(CustomersData(i), 1, 4))) = CurrentCustomerID Then

                ' The ID is set to the current highest + 1
                CurrentCustomerID = CurrentCustomerID + 1

                ' Store the new ID to the textbox
                txtCustomerID.Text = CurrentCustomerID

            End If

        Next

    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click

        ' Call the function to clear the textboxes
        ClearTextboxes()

    End Sub

    Private Sub ClearTextboxes()

        ' Clear all the textboxes
        txtCustomerID.Text = ""
        txtFirstName.Text = ""
        txtSurname.Text = ""
        txtEmailAddress.Text = ""
        txtPhoneNumber.Text = ""

    End Sub

End Class