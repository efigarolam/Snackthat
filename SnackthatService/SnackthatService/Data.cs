using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;

namespace SnackthatService
{
    /// <summary>
    /// This class deals with the information to be sent or received by the Web Service.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// An instance of ActiveRecord Object to call the stored procedures.
        /// </summary>
        public ActiveRecord aR;

        /// <summary>
        /// Constructor of the class, initialize the instance of the ActiveRecord Object
        /// </summary>
        public Data()
        {
            this.aR = new ActiveRecord();
        }

        /// <summary>
        /// Obtains all the Products assigned to a Seller
        /// </summary>
        /// <param name="idSeller">Int ID of the Seller</param>
        /// <returns>Returns a DataTable with all the rows of assigned Products</returns>
        public DataTable getProductsBySeller(int idSeller)
        {
            ArrayList parameter = new ArrayList();
            DataTable dt;
            parameter.Add(idSeller);

            dt = this.aR.callProcedure("getProductsBySeller", parameter);
            dt.TableName = "Productos";
            return dt;
        }

        /// <summary>
        /// Obtains all the Sellers of the application
        /// </summary>
        /// <returns>Returns a DataTable with all the rows of Sellers</returns>
        public DataTable getSellers()
        {
            DataTable dt;

            dt = this.aR.callProcedure("getAllSellersToWS");
            dt.TableName = "Vendedores";

            return dt;
        }

        /// <summary>
        /// Obtains the Route assigned to a Seller
        /// </summary>
        /// <param name="idSeller">Int ID of the Seller</param>
        /// <returns>Returns a DataTable with the row of the assigned Route</returns>
        public DataTable getRouteBySeller(int idSeller)
        {
            ArrayList parameter = new ArrayList();
            DataTable dt;
            parameter.Add(idSeller);

            dt = this.aR.callProcedure("getRouteBySeller", parameter);
            dt.TableName = "Ruta";
            return dt;
        }

        /// <summary>
        /// Obtains all the Customers assigned to a Route
        /// </summary>
        /// <param name="idRoute">Int ID of the Route</param>
        /// <returns>Returns a DataTable with all the rows of assigned Customers</returns>
        public DataTable getCustomersByRoute(int idRoute)
        {
            ArrayList parameter = new ArrayList();
            DataTable dt;
            parameter.Add(idRoute);

            dt = this.aR.callProcedure("getCustomers_CustomersRoutes_AddressesCustomer_ByRoute", parameter);
            dt.TableName = "Clientes_ClientesRutas_ClientesDirecciones";
            return dt;
        }

        /// <summary>
        /// Store all the information about to the Sells.
        /// </summary>
        /// <param name="dtProducts">DataTable with the left Products</param>
        /// <param name="dtSells">DataTable with all the done Sells</param>
        /// <param name="dtProductsSells">DataTable with all the sold Products</param>
        /// <param name="idRoute">Int ID of the current Route of the Seller</param>
        public void setSells(DataTable dtProducts, DataTable dtSells, DataTable dtProductsSells, int idRoute)
        {
            if (dtProducts != null && dtSells != null)
            {
                ArrayList parametersProducts, parametersSells, parametersProductsSells, parametersReports;
                DataTable idSell;
                DateTime sellDate;

                for (int i = 0; i < dtProducts.Rows.Count; i++)
                {
                    parametersProducts = new ArrayList();
                    parametersProducts.Add(dtProducts.Rows[i].ItemArray[0].ToString()); //IdProduct
                    parametersProducts.Add(dtProducts.Rows[i].ItemArray[3].ToString()); //Amount

                    this.aR.callProcedure("updateProductsFromSeller", parametersProducts);
                }

                if (dtSells != null)
                {
                    for (int i = 0; i < dtSells.Rows.Count; i++)
                    {
                        sellDate = Convert.ToDateTime(dtSells.Rows[i].ItemArray[5].ToString());

                        parametersReports = new ArrayList();
                        parametersReports.Add(idRoute); //idRouteReport

                        parametersSells = new ArrayList();
                        parametersSells.Add(dtSells.Rows[i].ItemArray[1].ToString()); //idCustomer
                        parametersSells.Add(dtSells.Rows[i].ItemArray[2].ToString()); //idSeller
                        parametersReports.Add(dtSells.Rows[i].ItemArray[2].ToString()); //idSellerReports
                        parametersSells.Add(dtSells.Rows[i].ItemArray[3].ToString()); //Amount
                        parametersSells.Add(dtSells.Rows[i].ItemArray[4].ToString()); //Total
                        parametersSells.Add(sellDate.ToString("yyyy:MM:dd")); //StartDate
                        parametersReports.Add(sellDate.ToString("yyyy:MM:dd")); //StartDateReport

                        idSell = this.aR.callProcedure("setSell", parametersSells);

                        for (int j = 0; j < dtProductsSells.Rows.Count; j++)
                        {
                            if (dtSells.Rows[i].ItemArray[0].ToString() == dtProductsSells.Rows[j].ItemArray[0].ToString() && idSell != null && idSell.Columns[0].Caption == "idSell")
                            {
                                try
                                {
                                    parametersReports.RemoveAt(4);
                                    parametersReports.RemoveAt(3);
                                }
                                catch (Exception ex)
                                {
                                }
                                parametersProductsSells = new ArrayList();
                                parametersProductsSells.Add(idSell.Rows[0].ItemArray[0].ToString()); //idSell
                                parametersProductsSells.Add(dtProductsSells.Rows[j].ItemArray[1].ToString()); //idProduct
                                parametersReports.Add(dtProductsSells.Rows[j].ItemArray[1].ToString()); //idProductReport
                                parametersProductsSells.Add(dtProductsSells.Rows[j].ItemArray[2].ToString()); //Amount
                                parametersReports.Add(dtProductsSells.Rows[j].ItemArray[3].ToString()); //TotalReport

                                this.aR.callProcedure("setProductsSell", parametersProductsSells);
                                this.aR.callProcedure("setDataReport", parametersReports);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Store all the new Customers created by the Seller
        /// </summary>
        /// <param name="dt">DataTable with the new Customers</param>
        /// <param name="idRoute">Int ID of the current Route of the Seller</param>
        public void setCustomers(DataTable dt, int idRoute)
        {
            if (dt != null)
            {
                ArrayList parametersCustomer, parametersCustomerAddress, parametersRouteCustomer;
                DataTable idAddressCustomer;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parametersCustomer = new ArrayList();
                    parametersCustomer.Add(dt.Rows[i].ItemArray[1].ToString()); //Name
                    parametersCustomer.Add(dt.Rows[i].ItemArray[2].ToString()); //LastName
                    parametersCustomer.Add(dt.Rows[i].ItemArray[3].ToString()); //eMail
                    parametersCustomer.Add(dt.Rows[i].ItemArray[4].ToString()); //RFC

                    this.aR.callProcedure("setCustomer", parametersCustomer);

                    parametersCustomerAddress = new ArrayList();
                    parametersCustomerAddress.Add(dt.Rows[i].ItemArray[0].ToString()); //IdCustomer
                    parametersCustomerAddress.Add(dt.Rows[i].ItemArray[5].ToString()); //Address
                    parametersCustomerAddress.Add(dt.Rows[i].ItemArray[6].ToString()); //Phone

                    idAddressCustomer = this.aR.callProcedure("setAddressesCustomer", parametersCustomerAddress);

                    if (idAddressCustomer != null && idAddressCustomer.Columns[0].Caption == "idAddressesCustomer" && Convert.ToInt16(idAddressCustomer.Rows[0].ItemArray[0].ToString()) > 0)
                    {
                        parametersRouteCustomer = new ArrayList();
                        parametersRouteCustomer.Add(idRoute);
                        parametersRouteCustomer.Add(idAddressCustomer.Rows[0].ItemArray[0].ToString());

                        this.aR.callProcedure("setCustomersRoutes", parametersRouteCustomer);
                    }
                }
            }
        }
    }
}