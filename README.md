- This project is divided into majorly 2 parts (At this point of time) - FrontEnd & Services

- Service:
  - Coupon Service
    - Mango.Services.Coupon.BusinessLogics
      - This project contains implementation logics of services for CRUS operations
    - Mango.Services.Coupon.DataContext
      - This project contains the database context, migration related file
      - You need to run the migration for this location
    - Mango.Services.Coupon.Models
      - This project contains the models, DTO, Mapper
    - Mango.Services.CouponApi
      - This project contains the controller, AppSettings.JSON and Program.cs
      - Please updated the server name in the Connection String here (In AppSettings.JSON) and run the add/run the migration
        - "ConnectionStrings": {
                "DefaultConnectionString" : "server=DESKTOP-L1O9QC9\\SQLEXPRESS; database=Db_Mango_Coupon; trusted_connection=True;
          }

  - Mango.Services.ProductAPI
    - This project contains all the files related to Product API
    - Please updated the server name in the Connection String here (In AppSettings.JSON) and run the add/run the migration
      - "ConnectionStrings": {
              "DefaultConnectionString" : "server=DESKTOP-L1O9QC9\\SQLEXPRESS; database=Db_Mango_Product; trusted_connection=True;
        }

  - Mango.Services.Coupon.AuthAPIs
    - This project contains all the files related to Authentication and Authorization API
    - Please updated the server name in the Connection String here (In AppSettings.JSON) and run the add/run the migration
      - "ConnectionStrings": {
              "DefaultConnectionString" : "server=DESKTOP-L1O9QC9\\SQLEXPRESS; database=Db_Mango_Identity; trusted_connection=True;
        }

 - Mango.Services.Shopping.Cart.API
   - This project contains all the files related to shopping cart API.
   - Please updated the server name in the Connection String here (In AppSettings.JSON) and run the add/run the migration
      - "ConnectionStrings": {
              "DefaultConnectionString" : "server=DESKTOP-L1O9QC9\\SQLEXPRESS; database=Db_Mango_Shopping_Cart; trusted_connection=True;
        }
        
