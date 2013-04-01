using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Michael Larsen 11-15-12
/// </summary>
public class BusinessTier
{
    public BusinessTier()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataSet EmptyDataSet()
    {
        DataSet ds = new DataSet();

        DataTable dt = new DataTable("ErrorTable");
        dt.Columns.Add("ErrorCode");
        dt.Columns.Add("ErrorDescription");
        dt.Columns.Add("ErrorMethod");
        dt.Columns.Add("OriginalTier");

        ds.Tables.Add(dt);

        DataTable dt2 = new DataTable("Data");
        ds.Tables.Add(dt2);
        return ds;
    }


    //*//////////  GOLF REGISTRATION METHODS ///////////

    public DataSet getGolfDetails(String eventID)
    {
        DataSet ds = EmptyDataSet();

        if (eventID == "1010")
        {

            ds.Tables[1].Columns.Add("eventID");
            ds.Tables[1].Columns.Add("recipientID");
            ds.Tables[1].Columns.Add("title");
            ds.Tables[1].Columns.Add("time");
            ds.Tables[1].Columns.Add("date");
            ds.Tables[1].Columns.Add("location");
            ds.Tables[1].Columns.Add("director");
            ds.Tables[1].Columns.Add("email");
            ds.Tables[1].Columns.Add("phone");
            ds.Tables[1].Columns.Add("description");
            ds.Tables[1].Columns.Add("details");
            ds.Tables[1].Columns.Add("active"); // How will this be formatted?  1 or 0?  yes or no?
            ds.Tables[1].Columns.Add("individualPrice");
            ds.Tables[1].Columns.Add("recipientImage");
            ds.Tables[1].Columns.Add("regCloseDate");
            ds.Tables[1].Columns.Add("address");
            ds.Tables[1].Columns.Add("city");
            ds.Tables[1].Columns.Add("state");
            ds.Tables[1].Columns.Add("zip");


            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1010";
            dataRow[1] = "6789";
            dataRow[2] = "Weber State Golf Tourney";
            dataRow[3] = "9:00 AM";
            dataRow[4] = "11/24/12";
            dataRow[5] = "305 West Pleasant View Drive, Pleasant View, UT 84414";
            dataRow[6] = "Michael Larsen";
            dataRow[7] = "mike@gmail.com";
            dataRow[8] = "801-971-4999";
            dataRow[9] = "Description - This golf tournament will go to benefit Rich Tompson.  It will be held at Weber State";
            dataRow[10] = "Details - Please meet at the main desk to check in.";
            dataRow[11] = "yes";  // Assuming "yes or "no" for now
            dataRow[12] = "25.00";
            dataRow[13] = "http://www.anythingforafriend.com/files/cache/e7f2c858f73cdbbbd7efea2d7a156fed.png";
            dataRow[14] = "12/25/12";
            dataRow[15] = "305 West Pleasant View Drive";
            dataRow[16] = "Pleasant View";
            dataRow[17] = "UT";
            dataRow[18] = "84414";

            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getGolfDetails";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }
        return ds;

    }

    
    public DataSet insertGolfRegistration(Dictionary<String, String> golfRegInfo)
    {
        DataSet ds = EmptyDataSet();

        if (golfRegInfo["eventID"] == "1010")
        {
            ds.Tables[1].Columns.Add("transactionID");
            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1234567890GUID";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "insertGolfRegistration";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;
    }

    public DataSet getGolfParticipants(String eventID)
    {
        DataSet ds = EmptyDataSet();

        if (eventID == "1010")
        {
            ds.Tables[1].Columns.Add("firstName");
            ds.Tables[1].Columns.Add("lastName");
            ds.Tables[1].Columns.Add("suffix");
            ds.Tables[1].Columns.Add("email");
            ds.Tables[1].Columns.Add("phone");
            ds.Tables[1].Columns.Add("numInParty");
            ds.Tables[1].Columns.Add("transactionID");
            ds.Tables[1].Columns.Add("cost");


            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Michael";
            dataRow[1] = "Larsen";
            dataRow[2] = "jr";
            dataRow[3] = "mike@stopChangingTheContract.com";
            dataRow[4] = "801-389-7118";
            dataRow[5] = "4";
            dataRow[6] = "0x0234234";
            dataRow[7] = "40";
            
            ds.Tables[1].Rows.Add(dataRow);

            dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Robert";
            dataRow[1] = "Hilton";
            dataRow[2] = "jr";
            dataRow[3] = "mike@stopChangingTheContract.com";
            dataRow[4] = "801-389-7118";
            dataRow[5] = "4";
            dataRow[6] = "0x0234234";
            dataRow[7] = "40";
            ds.Tables[1].Rows.Add(dataRow);

            dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Andrew";
            dataRow[1] = "Heim";
            dataRow[2] = "jr";
            dataRow[3] = "mike@stopChangingTheContract.com";
            dataRow[4] = "801-389-7118";
            dataRow[5] = "4";
            dataRow[6] = "0x0234234";
            dataRow[7] = "40";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0"; 
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getGolfParticipants";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;

    }

    public DataSet getMaxGolfParticipants(String eventID)
    {
        DataSet ds = EmptyDataSet();

        if (eventID == "1010")
        {
            ds.Tables[1].Columns.Add("maxParticipants");

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "75";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getMaxGolfParticipants";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }


        return ds;

    }


    public DataSet confirmGolfRegistration(Dictionary<String, String> golfConfirmInfo)
    {
        DataSet ds = EmptyDataSet();

        if (golfConfirmInfo["transactionID"] == "1234567890GUID" && golfConfirmInfo["eventID"] == "1010")
        {
            ds.Tables[1].Columns.Add("firstName");
            ds.Tables[1].Columns.Add("lastName");
            ds.Tables[1].Columns.Add("suffix");
            ds.Tables[1].Columns.Add("email");
            ds.Tables[1].Columns.Add("phone");
            ds.Tables[1].Columns.Add("numInParty");
            ds.Tables[1].Columns.Add("transactionID");
            ds.Tables[1].Columns.Add("cost");

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Jordan";
            dataRow[1] = "Ondrusek";
            dataRow[2] = "Mr.";
            dataRow[3] = "apheim@gmail.com";
            dataRow[4] = "801-123-4567";
            dataRow[5] = "4";
            dataRow[6] = "67890";
            dataRow[7] = "60.00";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "confirmGolfRegistration";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;


    }


    public DataSet validateGolfReg(Dictionary<String, String> golfRegInfo)
    {

        DataSet ds = EmptyDataSet();

        ds.Tables[1].Columns.Add("registrationInfo");

        //  Fill the results table with the info just submitted to ensure it was passed properly

        DataRow dataRow = ds.Tables[1].NewRow();
        dataRow[0] = golfRegInfo["eventID"];
        ds.Tables[1].Rows.Add(dataRow);

        dataRow = ds.Tables[1].NewRow();
        dataRow[0] = golfRegInfo["firstName"];
        ds.Tables[1].Rows.Add(dataRow);

        dataRow = ds.Tables[1].NewRow();
        dataRow[0] = golfRegInfo["lastName"];
        ds.Tables[1].Rows.Add(dataRow);

        dataRow = ds.Tables[1].NewRow();
        dataRow[0] = golfRegInfo["suffix"];
        ds.Tables[1].Rows.Add(dataRow);

        dataRow = ds.Tables[1].NewRow();
        dataRow[0] = golfRegInfo["email"];
        ds.Tables[1].Rows.Add(dataRow);

        dataRow = ds.Tables[1].NewRow();
        dataRow[0] = golfRegInfo["emailConfirm"];
        ds.Tables[1].Rows.Add(dataRow);

        dataRow = ds.Tables[1].NewRow();
        dataRow[0] = golfRegInfo["phone"];
        ds.Tables[1].Rows.Add(dataRow);

        dataRow = ds.Tables[1].NewRow();
        dataRow[0] = golfRegInfo["numInParty"];
        ds.Tables[1].Rows.Add(dataRow);

        if(golfRegInfo["email"] == "")
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "verifyGolfRegistration";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);

            DataRow errorRow2 = ds.Tables[0].NewRow();
            errorRow2[0] = "0";
            errorRow2[1] = "Generic Error Description 2";
            errorRow2[2] = "verifyGolfRegistration";
            errorRow2[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow2);
        }

        return ds;


    }




    //*///////// END GOLF REGISTRATION METHODS //////////




    //*///////// 5K REGISTRATION METHODS ////////////////


    public DataSet getRunDetails(String runEventID)
    {
        DataSet ds = EmptyDataSet();

        if (runEventID == "1337")
        {

            ds.Tables[1].Columns.Add("eventID");
            ds.Tables[1].Columns.Add("recipientID");
            ds.Tables[1].Columns.Add("title");
            ds.Tables[1].Columns.Add("time");
            ds.Tables[1].Columns.Add("date");
            ds.Tables[1].Columns.Add("location");
            ds.Tables[1].Columns.Add("director");
            ds.Tables[1].Columns.Add("email");
            ds.Tables[1].Columns.Add("phone");
            ds.Tables[1].Columns.Add("description");
            ds.Tables[1].Columns.Add("details");
            ds.Tables[1].Columns.Add("active"); //  How will this be formatted? "1" or "0", yes or no?
            ds.Tables[1].Columns.Add("childPrice"); // Added by Jason Vance, 20 Nov 2012
            ds.Tables[1].Columns.Add("adultPrice"); // Added by Jason Vance, 20 Nov 2012
            ds.Tables[1].Columns.Add("recipientImage"); // Added by Jason Vance, 19 Nov 2012
            ds.Tables[1].Columns.Add("regCloseDate");
            ds.Tables[1].Columns.Add("address");
            ds.Tables[1].Columns.Add("city");
            ds.Tables[1].Columns.Add("state");
            ds.Tables[1].Columns.Add("zip");

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1337";
            dataRow[1] = "6789";
            dataRow[2] = "Weber State 5K";
            dataRow[3] = "9:00 AM";
            dataRow[4] = "11/24/12";
            dataRow[5] = "Weber State University - 3848 Harrison Blvd Ogden, UT, 84408";
            dataRow[6] = "Michael Larsen";
            dataRow[7] = "mike@gmail.com";
            dataRow[8] = "801-971-4999";
            dataRow[9] = "Description - This 5K will go to benefit Tami Flannery.  It will be held at Weber State";
            dataRow[10] = "Details - Please meet at the Swenson gym.  The race runs through campus and ends back at Swenson gym.";
            dataRow[11] = "yes"; //Assuming "yes" or "no" for now
            dataRow[12] = "15.00"; // Added by Jason Vance, 20 Nov 2012
            dataRow[13] = "25.00"; // Added by Jason Vance, 20 Nov 2012
            dataRow[14] = "http://www.anythingforafriend.com/files/cache/686048a1cc32131bc3b5546b775fe36d.png"; // Added by Jason Vance, 19 Nov 2012
            dataRow[15] = "12/25/12";
            dataRow[16] = "3848 Harrison Blvd";
            dataRow[17] = "Ogden";
            dataRow[18] = "UT";
            dataRow[19] = "84408";

            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getRunDetails";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }
        return ds;
    }


    public DataSet getRunParticipants(String runEventID)
    {
        DataSet ds = EmptyDataSet();

        if (runEventID == "1337")
        {
            ds.Tables[1].Columns.Add("firstName");
            ds.Tables[1].Columns.Add("lastName");  

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Jimmy";
            dataRow[1] = "Cleveland";
        
            ds.Tables[1].Rows.Add(dataRow);

            dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Jordan";
            dataRow[1] = "Ondrusek";
           
            ds.Tables[1].Rows.Add(dataRow);

            dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Nathan";
            dataRow[1] = "Schroader";
           
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0"; 
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getRunParticipants";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;

    }

    public DataSet registerRunGroup(List<Dictionary<String, String>> listReg)
    {
        DataSet ds = EmptyDataSet();

        int count = listReg.Count;

        if (count > 0)
        {
            String sessionID = System.Guid.NewGuid().ToString("N");
            ds.Tables[1].Columns.Add("ID");
            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1234567890GUID";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0"; 
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "registerRunGroup";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);

        }
        
         return ds;
    }

    public DataSet confirmRunPayment(Dictionary<String, String> runRegisInfo)
    {
        DataSet ds = EmptyDataSet();

        if (runRegisInfo["transactionID"] == "1234567890GUID" && runRegisInfo["eventID"] == "1337")
        {
            ds.Tables[1].Columns.Add("eventID");
            ds.Tables[1].Columns.Add("firstName");
            ds.Tables[1].Columns.Add("lastName");
            ds.Tables[1].Columns.Add("suffix");
            ds.Tables[1].Columns.Add("shirtSize");
            ds.Tables[1].Columns.Add("phone");
            ds.Tables[1].Columns.Add("email");
            ds.Tables[1].Columns.Add("emailConfirm");
            ds.Tables[1].Columns.Add("emergContact");
            ds.Tables[1].Columns.Add("emergPh");
            ds.Tables[1].Columns.Add("transactionID");
            ds.Tables[1].Columns.Add("personID");
            ds.Tables[1].Columns.Add("cost");

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1337";
            dataRow[1] = "Jason";
            dataRow[2] = "Vance";
            dataRow[3] = "Mr.";
            dataRow[4] = "M";
            dataRow[5] = "801-123-4567";
            dataRow[6] = "apheim@gmail.com";
            dataRow[7] = "apheim@gmail.com";
            dataRow[8] = "Jose Canseco";
            dataRow[9] = "801-911-9111";
            dataRow[10] = "67890";
            dataRow[11] = "55555";
            dataRow[12] = "20.00";
            ds.Tables[1].Rows.Add(dataRow);

            DataRow dataRow2 = ds.Tables[1].NewRow();
            dataRow2[0] = "1337";
            dataRow2[1] = "Jason";
            dataRow2[2] = "Vance";
            dataRow2[3] = "Mr.";
            dataRow2[4] = "M";
            dataRow2[5] = "801-123-4567";
            dataRow2[6] = "jason@vance.com";
            dataRow2[7] = "jason@vance.com";
            dataRow2[8] = "Jose Canseco";
            dataRow2[9] = "801-911-9111";
            dataRow2[10] = "67890";
            dataRow2[11] = "55555";
            dataRow2[12] = "30.00";
            ds.Tables[1].Rows.Add(dataRow2);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "confirmRunPayment";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;


    }

    public DataSet confirmDonation(Dictionary<String, String> donationInfo)
    {
        DataSet ds = EmptyDataSet();

        if (donationInfo["transactionID"] == "1234567890GUID" && donationInfo["eventID"] == "1013")
        {
            ds.Tables[1].Columns.Add("firstName");
            ds.Tables[1].Columns.Add("lastName");
            ds.Tables[1].Columns.Add("suffix");
            ds.Tables[1].Columns.Add("phone");
            ds.Tables[1].Columns.Add("email");
            ds.Tables[1].Columns.Add("address");
            ds.Tables[1].Columns.Add("city");
            ds.Tables[1].Columns.Add("state");
            ds.Tables[1].Columns.Add("zip");
            ds.Tables[1].Columns.Add("cashValue");
            ds.Tables[1].Columns.Add("donationType");
            ds.Tables[1].Columns.Add("d");


            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Jason";
            dataRow[1] = "Vance";
            dataRow[2] = "Mr";
            dataRow[4] = "801-123-4567";
            dataRow[5] = "apheim@gmail.com";
            dataRow[6] = "1467 45th Street";
            dataRow[7] = "Ogden";
            dataRow[8] = "Utah";
            dataRow[9] = "84401";
            dataRow[10] = "50.00";
            dataRow[11] = "Monetary";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "confirmDonation";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;

    }
    
    public DataSet getMaxRunParticipants(String eventID)
    {
        DataSet ds = EmptyDataSet();

        if (eventID == "1337")
        {
            ds.Tables[1].Columns.Add("maxParticipants");

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "500";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getMaxRunParticipants";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }


        return ds;

    }

    public DataSet validateRunReg(Dictionary<String, String> runRegInfo)
    {
        DataSet ds = EmptyDataSet();
        runRegInfo["eventID"] = "1337";

        String phone = runRegInfo["phone"]; ;
        String emergPhone = runRegInfo["emergPh"]; ;

        if (runRegInfo["eventID"] == "1337" && phone.Length == 10 && emergPhone.Length == 10)
        {
            return ds;
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Invalid runner registration";
            errorRow[2] = "validateRunReg";
            errorRow[3] = "BusinessTier";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;
    }


    //*///////// END 5K REGISTRATION METHODS ///////////////








    //*///////// DINNER METHODS ///////////////////////////

    public DataSet getDinnerDetails(String eventID)
    {
        DataSet ds = EmptyDataSet();

        if (eventID == "12345")
        {

            ds.Tables[1].Columns.Add("eventID");
            ds.Tables[1].Columns.Add("recipientID");
            ds.Tables[1].Columns.Add("title");
            ds.Tables[1].Columns.Add("time");
            ds.Tables[1].Columns.Add("date");
            ds.Tables[1].Columns.Add("location");
            ds.Tables[1].Columns.Add("director");
            ds.Tables[1].Columns.Add("email");
            ds.Tables[1].Columns.Add("phone");
            ds.Tables[1].Columns.Add("description");
            ds.Tables[1].Columns.Add("details");
            ds.Tables[1].Columns.Add("active"); // How will this be formatted? "1" or "0", yes or no?
            ds.Tables[1].Columns.Add("childPrice");
            ds.Tables[1].Columns.Add("adultPrice");
            ds.Tables[1].Columns.Add("recipientImage");
            ds.Tables[1].Columns.Add("regCloseDate");
            ds.Tables[1].Columns.Add("address");
            ds.Tables[1].Columns.Add("city");
            ds.Tables[1].Columns.Add("state");
            ds.Tables[1].Columns.Add("zip");

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "12345";
            dataRow[1] = "6789";
            dataRow[2] = "Brandon Winger Dinner";
            dataRow[3] = "7:00 PM";
            dataRow[4] = "12/14/12"; 
            dataRow[5] = "Weber State University Ball Room - 3848 Harrison Blvd Ogden, UT, 84408";
            dataRow[6] = "Michael Larsen";
            dataRow[7] = "mike@gmail.com";
            dataRow[8] = "801-971-4999";
            dataRow[9] = "Anything for a Friend is proud to host a Banquet Dinner for Brandon Winger. " +
                "Brandon Winger was born in Ogden Utah on December 14, 1954. After graduating from Weber State University" +
                " with a degree in Fine Arts, Brandon Winger became an active member of the community. Brandon Winger was diagnosed with" +
                " type II pancreatic cancer in April 2012. ";
            dataRow[10] = "This event will feature two guest speakers: Ogden's Mayor Steve Nichols and Brandon Winger's brother John Barrett. " +
                "The Banquet Dinner will start promptly at 7:00 PM. Please arrive at the ball room 15 minutes prior to the event to checkin.";
            dataRow[11] = "yes"; //Assuming "yes" or "no" for now
            dataRow[12] = "10.00";
            dataRow[13] = "20.00";
            dataRow[14] = "http://www.anythingforafriend.com/files/cache/282119aa963c87f0c894dbe979a87986.png";
            dataRow[15] = "12/13/12";
            dataRow[16] = "3848 Harrison Blvd";
            dataRow[17] = "Ogden";
            dataRow[18] = "UT";
            dataRow[19] = "84408";
            ds.Tables[1].Rows.Add(dataRow);
         
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getDinnerDetails";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }
        return ds;
    }
        
    public DataSet insertDinnerRegistration(Dictionary<String, String> dinnerRegInfo)
    {
        DataSet ds = EmptyDataSet();

        if (dinnerRegInfo["eventID"] == "12345")
        {
            ds.Tables[1].Columns.Add("transactionID");
            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1234567890GUID";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0"; 
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "insertDinnerRegistration";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;
    }

    public DataSet getDinnerParticipants(String eventID)
    {
        DataSet ds = EmptyDataSet();

        if (eventID == "12345")
        {
            ds.Tables[1].Columns.Add("firstName");
            ds.Tables[1].Columns.Add("lastName");

            // added to reflect the contract method
            ds.Tables[1].Columns.Add("suffix");
			ds.Tables[1].Columns.Add("email");
			ds.Tables[1].Columns.Add("phone");
			ds.Tables[1].Columns.Add("numInParty");
			ds.Tables[1].Columns.Add("numChildren");
			ds.Tables[1].Columns.Add("numAdults");
			ds.Tables[1].Columns.Add("transactionID");
			ds.Tables[1].Columns.Add("cost");

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Aaron";
            dataRow[1] = "Copeland";
			dataRow[2] = "";
            dataRow[3] = "aaron@copeland.com";
			dataRow[4] = "801-123-4567";
			dataRow[5] = "4";
			dataRow[6] = "1";
			dataRow[7] = "3";
			dataRow[8] = "67890";
			dataRow[9] = "70.00";
            ds.Tables[1].Rows.Add(dataRow);

            dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Olivia";
            dataRow[1] = "Johnson";
            dataRow[2] = "";
            dataRow[3] = "olivia@johnson.com";
			dataRow[4] = "801-123-4567";
			dataRow[5] = "6";
			dataRow[6] = "2";
			dataRow[7] = "4";
			dataRow[8] = "67891";
			dataRow[9] = "100.00";
            ds.Tables[1].Rows.Add(dataRow);

            dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Jason";
            dataRow[1] = "Vance";
            dataRow[2] = "";
            dataRow[3] = "jason@vance.com";
			dataRow[4] = "801-123-4567";
			dataRow[5] = "2";
			dataRow[6] = "0";
			dataRow[7] = "2";
			dataRow[8] = "67892";
			dataRow[9] = "40.00";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getDinnerParticipants";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;
    }


    public DataSet getMaxDinnerParticipants(String eventID)
    {
        DataSet ds = EmptyDataSet();

        if (eventID == "12345")
        {
            ds.Tables[1].Columns.Add("maxParticipants");

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "150";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getMaxDinnerParticipants";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;
    }

    public DataSet confirmDinnerRegistration(Dictionary<String, String> confirmDinnerInfo)
    {
        DataSet ds = EmptyDataSet();

        if (confirmDinnerInfo["transactionID"] == "1234567890GUID" && confirmDinnerInfo["eventID"] == "12345")
        {
            
            ds.Tables[1].Columns.Add("firstName");
            ds.Tables[1].Columns.Add("lastName");
            ds.Tables[1].Columns.Add("suffix");
            ds.Tables[1].Columns.Add("email");
            ds.Tables[1].Columns.Add("phone");
            ds.Tables[1].Columns.Add("numInParty");
            ds.Tables[1].Columns.Add("numChildren");
            ds.Tables[1].Columns.Add("numAdults");
            ds.Tables[1].Columns.Add("transactionID");
            ds.Tables[1].Columns.Add("cost");

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "Aaron";
            dataRow[1] = "Copeland";
            dataRow[2] = "Mr.";
            dataRow[3] = "apheim@gmail.com";
            dataRow[4] = "801-123-4567";
            dataRow[5] = "4";
            dataRow[6] = "2";
            dataRow[7] = "2";
            dataRow[8] = "67890";
            dataRow[9] = "60.00";
            ds.Tables[1].Rows.Add(dataRow);

        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "confirmDinnerRegistration";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;
    }

    public DataSet validateDinnerReg(Dictionary<String, String> dinnerRegInfo)
    {
        DataSet ds = EmptyDataSet();
        dinnerRegInfo["eventID"] = "12345";
        if (dinnerRegInfo["eventID"] == "12345")
        {
            return ds;
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "verifyDinnerRegistration";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;
    }

    public DataSet getAllEvents()
    {
        DataSet ds = EmptyDataSet();

            ds.Tables[1].Columns.Add("eventID");
            ds.Tables[1].Columns.Add("recipientID");
            ds.Tables[1].Columns.Add("title");
            ds.Tables[1].Columns.Add("time");
            ds.Tables[1].Columns.Add("date");
            ds.Tables[1].Columns.Add("location");
            ds.Tables[1].Columns.Add("director");
            ds.Tables[1].Columns.Add("email");
            ds.Tables[1].Columns.Add("phone");
            ds.Tables[1].Columns.Add("description");
            ds.Tables[1].Columns.Add("details");
            ds.Tables[1].Columns.Add("active");
            ds.Tables[1].Columns.Add("recipientImage");
     

            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1337";
            dataRow[1] = "6789";
            dataRow[2] = "5k For Tami Flannery!";
            dataRow[3] = "7:00 PM";
            dataRow[4] = "11/24/12";
            dataRow[5] = "Weber State University Ball Room";
            dataRow[6] = "Michael Larsen";
            dataRow[7] = "mike@gmail.com";
            dataRow[8] = "801-971-4999";
            dataRow[9] = "Description - This banquet will go to benefit [RECIPIENT].  It will be held at Weber State";
            dataRow[10] = "Weber State was overwhelmed with love and support for Tami and her family on Saturday the 8th and after a beautiful morning and early afternoon full of touching expressions of love for the Flanery's, we are so happy to report that over $32,000 was generated at the event.  Many thanks to the outstanding organizing committee lead by Ms. Susie!  You are an unforgettable group and the stories coming out of the planning and execution of this event will be used to inspire groups for years to come.  Thank you all for literally doing ANYTHING FOR A FRIEND";
            //dataRow[10] = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam justo arcu, vestibulum vitae adipiscing non, tempus vitae dolor. Fusce at rutrum nibh. Donec bibendum, ipsum et dictum ultricies, eros leo varius risus, a mollis elit magna et erat. Nulla vestibulum vehicula lorem, nec suscipit enim sagittis quis. Vestibulum tincidunt iaculis urna ut facilisis. Aliquam erat volutpat. Sed eget lectus leo. Morbi sit amet tellus ac diam accumsan aliquam non imperdiet ante. Maecenas laoreet nisl nec neque tristique quis tincidunt nulla volutpat. Curabitur interdum feugiat magna. Cras ut nisl vel metus congue feugiat quis sit amet justo. Donec posuere tempus mi, quis pretium lectus sodales ac. Maecenas sodales egestas lacus sed luctus. Sed adipiscing pretium ante, id pellentesque massa ultricies eu. ";
            dataRow[11] = "yes"; //Assuming "yes" or "no" for now
            dataRow[12] = "http://www.anythingforafriend.com/files/cache/686048a1cc32131bc3b5546b775fe36d.png";
            ds.Tables[1].Rows.Add(dataRow);

            dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "12345";
            dataRow[1] = "6789";
            dataRow[2] = "Brandon Winger Dinner";
            dataRow[3] = "7:00 PM";
            dataRow[4] = "11/24/12";
            dataRow[5] = "Weber State University Ball Room";
            dataRow[6] = "Michael Larsen";
            dataRow[7] = "mike@gmail.com";
            dataRow[8] = "801-971-4999";
            dataRow[9] = "Description - This banquet will go to benefit Brandon Winger.  It will be held at Weber State";
            dataRow[10] = "Imagine going in for routine shots to enter into 7th grade only to learn that you have an unexplained mass in your stomach.  This fearful “routine” visit to the doctor was indeed Brandon’s new reality and the mass was later revealed to be a 5 1/2 by 5 1/2 inch tumor in his small intestine.  When the tumor was removed, it was determined to be a Spindle Cell Tumor.  As Brandon’s fellow classmates were excitedly getting their class schedules and beginning the journey into Junior High School, he was undergoing surgery for a tumor only known to be in men in their 40’s and has not been seen in 20 years!";
            //dataRow[10] = "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque eleifend elit sit amet nisl facilisis pharetra. Vestibulum ut auctor est. Nam tempus leo felis. Morbi purus purus, commodo et egestas ultricies, consequat id lorem. Mauris ac massa velit. Aliquam placerat feugiat aliquet. Aenean vel augue a urna vehicula tristique. Donec viverra congue nisl, sodales cursus ante faucibus eget. Nunc non vehicula lacus. Donec at orci velit. Aenean venenatis, leo a aliquam condimentum, urna leo auctor urna, in feugiat dui diam vel tortor. Praesent faucibus dui id lacus interdum ultricies. ";
            dataRow[11] = "yes"; //Assuming "yes" or "no" for now
            dataRow[12] = "http://www.anythingforafriend.com/files/cache/282119aa963c87f0c894dbe979a87986.png";
            ds.Tables[1].Rows.Add(dataRow);

            dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1013";
            dataRow[1] = "6789";
            dataRow[2] = "Donate to Kaleb Heaps";
            dataRow[3] = "7:00 PM";
            dataRow[4] = "11/12/12";
            dataRow[5] = "Weber State University Ball Room";
            dataRow[6] = "Michael Larsen";
            dataRow[7] = "mike@gmail.com";
            dataRow[8] = "801-971-4999";
            dataRow[9] = "Description - This banquet will go to benefit Kaleb Heaps.  It will be held at Weber State";
            dataRow[10] = "Kaleb is not unlike most of the 9-year-olds that you may know. He has two older brothers and one younger sister. He loves tackle football, snowboarding, riding motorcycles, cub scouts, and playing games with his family and friends.  On the other hand, he may be unlike most other 9 years that you know as he is in a battle for his life!";            
            //dataRow[10] = "Pellentesque egestas tortor et mi mattis commodo et vel odio. Pellentesque fermentum ultrices condimentum. Fusce eget massa enim, ut laoreet leo. Nunc fringilla, libero sit amet pulvinar malesuada, augue quam rutrum tellus, ac ultrices lacus augue fermentum quam. Curabitur nec neque sed est auctor viverra. Donec rutrum, nulla quis convallis tincidunt, massa leo egestas lorem, lobortis consequat turpis dui non libero. Proin in nibh sit amet quam lobortis bibendum dapibus eget quam. Maecenas in tortor sit amet eros auctor convallis vitae et est. In nec velit in nisl blandit vestibulum ut vel orci. Vestibulum eu ante nec nunc auctor imperdiet sit amet sit amet ligula. Vestibulum felis tortor, accumsan eu porttitor id, iaculis ut mi. Etiam elit nibh, dapibus eu fermentum non, rhoncus vel magna. Suspendisse placerat, lectus sit amet dapibus ullamcorper, velit nibh tempus est, a molestie sapien lorem non enim. Nulla ac nulla eu nunc accumsan tincidunt id at lacus. Nullam nec scelerisque elit. ";
            dataRow[11] = "yes"; //Assuming "yes" or "no" for now
            dataRow[12] = "http://www.anythingforafriend.com/files/cache/a9ff75621da82604821611a1ccf7736c.png";
            ds.Tables[1].Rows.Add(dataRow);

            dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1010";
            dataRow[1] = "6789";
            dataRow[2] = "Rich Thompson's Golf Tournament";
            dataRow[3] = "7:00 PM";
            dataRow[4] = "11/24/12";
            dataRow[5] = "Weber State University Ball Room";
            dataRow[6] = "Michael Larsen";
            dataRow[7] = "mike@gmail.com";
            dataRow[8] = "801-971-4999";
            dataRow[9] = "Description - This banquet will go to benefit [RECIPIENT].  It will be held at Weber State";
            dataRow[10] = "Life can stop on a dime!  Just when Rich, Christel and their two beautiful kids had life by the tail, Rich was struck by Stage IV Pancreatic Cancer in May 2012, but this is not the story his loved ones would like you to know about him! ";            
            //dataRow[10] = "Pellentesque egestas tortor et mi mattis commodo et vel odio. Pellentesque fermentum ultrices condimentum. Fusce eget massa enim, ut laoreet leo. Nunc fringilla, libero sit amet pulvinar malesuada, augue quam rutrum tellus, ac ultrices lacus augue fermentum quam. Curabitur nec neque sed est auctor viverra. Donec rutrum, nulla quis convallis tincidunt, massa leo egestas lorem, lobortis consequat turpis dui non libero. Proin in nibh sit amet quam lobortis bibendum dapibus eget quam. Maecenas in tortor sit amet eros auctor convallis vitae et est. In nec velit in nisl blandit vestibulum ut vel orci. Vestibulum eu ante nec nunc auctor imperdiet sit amet sit amet ligula. Vestibulum felis tortor, accumsan eu porttitor id, iaculis ut mi. Etiam elit nibh, dapibus eu fermentum non, rhoncus vel magna. Suspendisse placerat, lectus sit amet dapibus ullamcorper, velit nibh tempus est, a molestie sapien lorem non enim. Nulla ac nulla eu nunc accumsan tincidunt id at lacus. Nullam nec scelerisque elit. ";
            dataRow[11] = "yes"; //Assuming "yes" or "no" for now
            dataRow[12] = "http://www.anythingforafriend.com/files/cache/e7f2c858f73cdbbbd7efea2d7a156fed.png";
            ds.Tables[1].Rows.Add(dataRow);
            
        return ds;

    }

    public DataSet getEventType(string eventID)
    {
        DataSet ds = EmptyDataSet();

        ds.Tables[1].Columns.Add("eventType");
        DataRow dataRow = ds.Tables[1].NewRow();
        if(eventID == "12345")
           dataRow[0] = "Dinner";
        else if (eventID == "1337")
            dataRow[0] = "Run";
        else if (eventID == "1013")
            dataRow[0] = "Donation";
        else if (eventID == "1010")
            dataRow[0] = "Golf";
        ds.Tables[1].Rows.Add(dataRow);
        return ds;
    }

    //*///////// DONATION METHODS ///////////////////////////

    public DataSet validateDonationInfo(Dictionary<String, String> donationInfo)
    {
        DataSet ds = EmptyDataSet();
        donationInfo["eventID"] = "1013";

        if (donationInfo["eventID"] == "1013")
        {
            return ds;
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "verifyDinnerRegistration";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;
    }

    public DataSet insertDonation(Dictionary<String, String> golfRegInfo)
    {
        DataSet ds = EmptyDataSet();

        if (golfRegInfo["eventID"] == "1013")
        {
            ds.Tables[1].Columns.Add("transactionID");
            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1234567890GUID";
            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "insertDonation";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }

        return ds;
    }

    public DataSet getDonationDetails(String eventID)
    {
        DataSet ds = EmptyDataSet();

        if (eventID == "1013")
        {
            ds.Tables[1].Columns.Add("eventID");
            ds.Tables[1].Columns.Add("recipientID");
            ds.Tables[1].Columns.Add("title");
            ds.Tables[1].Columns.Add("description");
            ds.Tables[1].Columns.Add("details");
            ds.Tables[1].Columns.Add("active");            
            ds.Tables[1].Columns.Add("recipientImage");


            DataRow dataRow = ds.Tables[1].NewRow();
            dataRow[0] = "1013";
            dataRow[1] = "6789";
            dataRow[2] = "Donate to Kaleb Heaps";
            dataRow[3] = "Anything for a Friend is proud to announce their newest recipient Kaleb Heaps. " +
                "Kaleb Heaps was born in Ogden Utah on December 14, 1954. After graduating from Weber State University" +
                " with a degree in Fine Arts, Kaleb Heaps became an active member of the community. Kaleb Heaps was diagnosed with" +
                " type II pancreatic cancer in April 2012. ";
            dataRow[4] = "Donations will be for Kaleb Heaps's cancer treatment at the Huntsman Cancer Institute and family support.";
            dataRow[5] = "yes"; //Assuming "yes" or "no" for now            
            dataRow[6] = "http://www.anythingforafriend.com/files/cache/a9ff75621da82604821611a1ccf7736c.png";

            ds.Tables[1].Rows.Add(dataRow);
        }
        else
        {
            DataRow errorRow = ds.Tables[0].NewRow();
            errorRow[0] = "0";
            errorRow[1] = "Generic Error Description";
            errorRow[2] = "getDinnerDetails";
            errorRow[3] = "Business";
            ds.Tables[0].Rows.Add(errorRow);
        }
        return ds;
    }
}