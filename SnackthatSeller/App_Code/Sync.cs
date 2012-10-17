using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using SignalR;

/// <summary>
/// Descripción breve de Sync
/// </summary>

namespace SignalRTest
{
    public class Sync : SignalR.Hubs.Hub
    {

        SnackthatServiceClient.SnackthatServiceSoapClient snackthatClient;
        ActiveRecord aR;

        public Sync()
        {
            this.snackthatClient = new SnackthatServiceClient.SnackthatServiceSoapClient();
            this.aR = new ActiveRecord();            
        }

        public void receiveData(int idSeller)
        {
            try
            {
                Clients.addMessage(0);

                this.snackthatClient = new SnackthatServiceClient.SnackthatServiceSoapClient();
                this.aR = new ActiveRecord();

                Clients.addMessage(5);
                DataTable dtUsers = this.snackthatClient.getSellers();

                if (dtUsers != null)
                {
                    this.aR.callProcedure("deleteUsers");

                    for (int i = 0; i < dtUsers.Rows.Count; i++)
                    {
                        ArrayList parametersUser = new ArrayList();

                        parametersUser.Add(dtUsers.Rows[i].ItemArray[0].ToString());
                        parametersUser.Add(dtUsers.Rows[i].ItemArray[1].ToString());
                        parametersUser.Add(dtUsers.Rows[i].ItemArray[2].ToString());
                        parametersUser.Add(dtUsers.Rows[i].ItemArray[3].ToString());
                        parametersUser.Add(dtUsers.Rows[i].ItemArray[4].ToString());
                        parametersUser.Add(dtUsers.Rows[i].ItemArray[5].ToString());
                        parametersUser.Add(dtUsers.Rows[i].ItemArray[6].ToString());
                        parametersUser.Add(dtUsers.Rows[i].ItemArray[7].ToString());
                        parametersUser.Add(dtUsers.Rows[i].ItemArray[8].ToString());
                        parametersUser.Add(dtUsers.Rows[i].ItemArray[9].ToString());

                        this.aR.callProcedure("setUser", parametersUser);
                    }
                }

                Clients.addMessage(15);
                DataTable dtProducts = this.snackthatClient.getProductsBySeller(idSeller);

                Clients.addMessage(25);
                if (dtProducts != null)
                {
                    this.aR.callProcedure("deleteProductsContent");
                    Clients.addMessage(30);
                    for (int i = 0; i < dtProducts.Rows.Count; i++)
                    {
                        ArrayList parametersProduct = new ArrayList();

                        parametersProduct.Add(dtProducts.Rows[i].ItemArray[0].ToString()); //ID
                        parametersProduct.Add(dtProducts.Rows[i].ItemArray[2].ToString()); //Presentation
                        parametersProduct.Add(dtProducts.Rows[i].ItemArray[1].ToString()); //Name
                        parametersProduct.Add(dtProducts.Rows[i].ItemArray[5].ToString()); //Amount
                        parametersProduct.Add(dtProducts.Rows[i].ItemArray[3].ToString()); //Price
                        parametersProduct.Add(HttpUtility.HtmlDecode(dtProducts.Rows[i].ItemArray[4].ToString())); //Description

                        this.aR.callProcedure("setProduct", parametersProduct);
                    }
                }

                Clients.addMessage(50);
                DataTable dtRoute = this.snackthatClient.getRouteBySeller(idSeller); //id
                ArrayList parametersRoute = new ArrayList();

                Clients.addMessage(65);
                if (dtRoute != null)
                {
                    parametersRoute.Add(dtRoute.Rows[0].ItemArray[0].ToString()); //ID
                    parametersRoute.Add(dtRoute.Rows[0].ItemArray[1].ToString()); //Name

                    this.aR.callProcedure("setRoute", parametersRoute);
                }

                Clients.addMessage(75);
                DataTable dtCustomers = this.snackthatClient.getCustomersByRoute(int.Parse(parametersRoute[0].ToString())); //id

                Clients.addMessage(80);
                if (dtCustomers != null)
                {
                    for (int i = 0; i < dtCustomers.Rows.Count; i++)
                    {
                        ArrayList parametersCustomer = new ArrayList();
                        parametersCustomer.Add(dtCustomers.Rows[i].ItemArray[0].ToString()); //IdCustomer
                        parametersCustomer.Add(dtCustomers.Rows[i].ItemArray[1].ToString()); //Name
                        parametersCustomer.Add(dtCustomers.Rows[i].ItemArray[2].ToString()); //LastName
                        parametersCustomer.Add(dtCustomers.Rows[i].ItemArray[3].ToString()); //eMail
                        parametersCustomer.Add(dtCustomers.Rows[i].ItemArray[4].ToString()); //RFC

                        //Parametros de la tabla addressescustomer
                        ArrayList parametersCustomerAddresses = new ArrayList();
                        parametersCustomerAddresses.Add(dtCustomers.Rows[i].ItemArray[5].ToString()); //IdAddressCustomer
                        parametersCustomerAddresses.Add(dtCustomers.Rows[i].ItemArray[6].ToString()); //IdCustomer
                        parametersCustomerAddresses.Add(dtCustomers.Rows[i].ItemArray[7].ToString()); //Address
                        parametersCustomerAddresses.Add(dtCustomers.Rows[i].ItemArray[8].ToString()); //Phone

                        //Parametros de la tabla customersroutes
                        ArrayList parametersRouteCustomers = new ArrayList();
                        parametersRouteCustomers.Add(dtCustomers.Rows[i].ItemArray[9].ToString()); //IdCustomerRoute
                        parametersRouteCustomers.Add(dtCustomers.Rows[i].ItemArray[10].ToString()); //IdRoute
                        parametersRouteCustomers.Add(dtCustomers.Rows[i].ItemArray[11].ToString()); //IdAddressCustomer
                        parametersRouteCustomers.Add(dtCustomers.Rows[i].ItemArray[12].ToString()); //EstimatedTime

                        try
                        {
                            parametersRouteCustomers[parametersRouteCustomers.Count - 1] = Convert.ToDateTime(parametersRouteCustomers[parametersRouteCustomers.Count - 1].ToString());
                        }
                        catch (Exception ex)
                        {
                            parametersRouteCustomers[parametersRouteCustomers.Count - 1] = null;
                        }

                        //Se llenan las tablas de customer, addressescustomers y customersroutes
                        this.aR.callProcedure("setCustomer", parametersCustomer);
                        this.aR.callProcedure("setAddressesCustomer", parametersCustomerAddresses);
                        this.aR.callProcedure("setCustomersRoutes", parametersRouteCustomers);
                    }
                    Clients.addMessage(100);
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                Clients.addMessage(1000);
            }
            catch (Exception ex)
            {
                Clients.addMessage(3000);
            }
        }

        public void sendData()
        {
            try
            {
                Clients.addMessage(0);

                this.snackthatClient = new SnackthatServiceClient.SnackthatServiceSoapClient();
                this.aR = new ActiveRecord();

                Clients.addMessage(25);
                DataTable dt = new Customers().getAllCustomers(1);
                DataTable route = new Routes().getAllRoutes();

                Clients.addMessage(50);
                dt.TableName = "ClientesChofer";
                this.snackthatClient.setCustomers(dt, Convert.ToInt16(route.Rows[0].ItemArray[0].ToString()));

                DataTable dtProducts = new Products().getAllProducts(), dtSells = this.aR.callProcedure("getAllSells"), dtProductsSells = this.aR.callProcedure("getAllProductsSell");
                Clients.addMessage(75);

                dtProducts.TableName = "Productos";
                dtSells.TableName = "RegistroVentas";
                dtProductsSells.TableName = "ProductsSells";
                this.snackthatClient.setSells(dtProducts, dtSells, dtProductsSells, Convert.ToInt16(route.Rows[0].ItemArray[0].ToString()));

                Clients.addMessage(90);
                this.aR.callProcedure("cleanAll");
                Clients.addMessage(100);
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                Clients.addMessage(1000);
            }
            catch (Exception ex)
            {
                Clients.addMessage(2000);
            }
        }
    }
}