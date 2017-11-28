using System;

namespace FlyerMe.Controls.CreateFlyer.WizardSteps
{
    public partial class Description : CreateFlyerWizardControlBase
    {
        protected Boolean IsZillowBlockVisible
        {
            get
            {
                return Flyer.FlyerType == FlyerTypes.Seller;
            }
        }

        protected void Page_Load(Object sender, EventArgs args)
        {
            InitFields(Profile);
            SetInputs();
            form.Action = FormAction;
        }

        public override void SetSessionData(Boolean isInitial)
        {
            InitFields(Profile);

            if (!isInitial)
            {
                Flyer.MlsNumber = Request["mlsnumber"];
                Flyer.YearBuilt = Request["yearbuilt"];
                Flyer.Subdivision = Request["subdivision"];
                Flyer.Hoa = Request["hoa"];
                Flyer.Bedrooms = Request["bedrooms"];
                Flyer.Bathrooms = Request["bathrooms"];
                Flyer.LotSize = Request["lotsize"];
                Flyer.Sqft = Request["sqft"];
                Flyer.Floors = Request["floors"];
                Flyer.Parking = Request["parking"];
                Flyer.OpenHouses = Request["openhouses"];
                Flyer.Description = Request["description"];
                Flyer.DescriptionStepCompleted = true;
            }
        }

        #region private

        private void SetInputs()
        {
            inputMlsNumber.Value = Flyer.MlsNumber;
            inputYearBuilt.Value = Flyer.YearBuilt;
            inputSubdivision.Value = Flyer.Subdivision;
            inputHoa.Value = Flyer.Hoa;
            inputLotSize.Value = Flyer.LotSize;
            inputSqft.Value = Flyer.Sqft;
            inputOpenHouses.Value = Flyer.OpenHouses;
            textareaDescription.Value = Flyer.Description;
            SetDdlBedroomsItems();
            SetDdlBathroomsItems();
            SetDdlFloorsItems();
            SetDdlParkingItems();
            SetDdlSelectedIndex(ddlBedrooms, Flyer.Bedrooms);
            SetDdlSelectedIndex(ddlBathrooms, Flyer.Bathrooms);
            SetDdlSelectedIndex(ddlFloors, Flyer.Floors);
            SetDdlSelectedIndex(ddlParking, Flyer.Parking);
            divOpenHouses.Visible = Flyer.FlyerType == FlyerTypes.Seller;
        }

        private void SetDdlBedroomsItems()
        {
            if (ddlBedrooms.Items.Count > 0)
            {
                return;
            }

            ddlBedrooms.Items.Add("Studio");
            
            for(var i = 0; i < 25; i++)
            {
                ddlBedrooms.Items.Add(i.ToString());
            }

            ddlBedrooms.Items.Add("25+");

            if (String.IsNullOrEmpty(Flyer.Bedrooms))
            {
                ddlBedrooms.SelectedIndex = 1;
            }
        }

        private void SetDdlBathroomsItems()
        {
            if (ddlBathrooms.Items.Count > 0)
            {
                return;
            }

            for (var i = 0; i < 25; i++)
            {
                ddlBathrooms.Items.Add(i.ToString());
            }

            ddlBathrooms.Items.Add("25+");
        }

        private void SetDdlFloorsItems()
        {
            if (ddlFloors.Items.Count > 0)
            {
                return;
            }

            for (var i = 0; i < 5; i++)
            {
                ddlFloors.Items.Add(i.ToString());
            }

            ddlFloors.Items.Add("5+");
        }

        private void SetDdlParkingItems()
        {
            if (ddlParking.Items.Count > 0)
            {
                return;
            }

            ddlParking.Items.Add("0");

            for (var i = 1; i < 51; i++)
            {
                ddlParking.Items.Add(i.ToString() + " Cars");
            }
        }

        #endregion
    }
}
