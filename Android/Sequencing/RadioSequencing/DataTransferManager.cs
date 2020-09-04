using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using NetworkDetection;
using System.Threading.Tasks;
//using AccesoDatosNet;
using System.Threading;
using AccesoDatosXML;
using Xamarin.Essentials;

namespace RadioSequencing
{
    [Service]
    public class DataTransferManager : Service
    {
        private cAccesoDatosXML ServiceConnection = new cAccesoDatosXML(Values.gDatos) { DataBase="PROCESOS" };
        public NetworkStatusMonitor Monitor { get; set; }
        //public Context Context { get; set; }
        public bool Transmitting { get; private set; } = false;
        //public DataTransferManager(Context _context)
        //{
        //    context = _context;
        //    monitor = new NetworkStatusMonitor(context);
        //    monitor.NetworkStatusChanged += Monitor_NetworkStatusChanged;
        //    monitor.Start();
        //}
        public static bool Active { get; set; } = false;
        private async void Monitor_NetworkStatusChanged(object sender, EventArgs e)
        {
            if (Monitor.State != NetworkState.ConnectedData && Monitor.State != NetworkState.ConnectedWifi)
            {
                await Values.sFt.commProgressStatus(ProgressStatusEnum.DISCONNECTED);
            } else
            {
                await Values.sFt.commProgressStatus(ProgressStatusEnum.CONNECTED);
            }
        }

        public async Task DoWork()
        {
            while (true)
            {
                SpinWait.SpinUntil(() => Active);
                Thread.Sleep(5000);
                await Transfer();
            }
        }


        public async Task Transfer()
        {
            if (Transmitting)
                return;
            Transmitting = true;
            //readings
            while (true)
            {
                try
                {
                    var query = await Values.SQLidb.db.Table<ScannedData>().Where(r => r.Transmitted == false).ToListAsync();
                    if (query.Count == 0)
                        break;
                    foreach (var r in query)
                    {
                        //show the list in the debug fragment
                        //var q = await Values.SQLidb.db.Table<ScannedData>().Where(z => z.Transmitted == false).ToListAsync();
                        //Values.dFt.Clear();
                        //q.ForEach(async z => await Values.dFt.pushInfo(z.Action, z.LabelRack+z.Serial, z.Partnumber, z.Qty.ToString()));
                        //Thread.Sleep(500);
                        if (Monitor.State == NetworkState.ConnectedData || Monitor.State == NetworkState.ConnectedWifi)
                        {
                            //Values.sFt.socksProgress.Visibility = ViewStates.Visible;
                            
                            using (SPXML _sp = new SPXML(ServiceConnection, "pLaunchProcess_ReadingSessionControl"))
                            {
                                //SPXML _sp = new SPXML(ServiceConnection, "pLaunchProcess_ReadingSessionControl");
                                _sp.AddParameterValue("@DB", "SEQUENCING");
                                _sp.AddParameterValue("@ProcedureName", "pSequencingSessionDetAdd");
                                _sp.AddParameterValue("@Parameters", r.ProcedureParameters());
                                _sp.AddParameterValue("@TableDB", "");
                                _sp.AddParameterValue("@TableName", "");
                                _sp.AddParameterValue("@TablePK", "");
                                try
                                {
                                    await _sp.ExecuteAsync();
                                    if (_sp.LastMsg.Substring(0, 2) != "OK")
                                    {
                                        Transmitting = false;
                                        return;
                                    }
                                    else
                                    {
                                        r.Transmitted = true;
                                        await Values.SQLidb.db.UpdateAsync(r);
                                        Values.sFt.UpdateInfo();
                                        //switch (r.Action)
                                        //{
                                        //    case "CHECK":
                                        //        Values.sFt.CheckQtyTransmitted++;
                                        //        break;
                                        //    case "ADD":
                                        //        Values.sFt.ReadQtyTransmitted++;
                                        //        break;
                                        //}
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Transmitting = false;
                                    Values.dFt.SetMessage(ex.Message);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Transmitting = false;
                            return;
                        }

                    }
                }
                catch
                {
                    Transmitting = false;
                    return;
                }
            }
            //location data
            while (true)
            {
                try
                {
                    var query = await Values.SQLidb.db.Table<DeviceLocation>().Where(r => r.Transmitted == false).ToListAsync();
                    if (query.Count == 0)
                        break;
                    foreach (var r in query)
                    {
                        //show the list in the debug fragment
                        //var q = await Values.SQLidb.db.Table<ScannedData>().Where(z => z.Transmitted == false).ToListAsync();
                        //Values.dFt.Clear();
                        //q.ForEach(async z => await Values.dFt.pushInfo(z.Action, z.LabelRack+z.Serial, z.Partnumber, z.Qty.ToString()));
                        //Thread.Sleep(500);
                        if (Monitor.State == NetworkState.ConnectedData || Monitor.State == NetworkState.ConnectedWifi)
                        {
                            //Values.sFt.socksProgress.Visibility = ViewStates.Visible;
                            //ServiceConnection.DataBase = "PROCESOS";
                            using (SPXML _sp = new SPXML(ServiceConnection, "pLaunchProcess_ReadingSessionControl"))
                            {
                                //SPXML _sp = new SPXML(ServiceConnection, "pLaunchProcess_ReadingSessionControl");
                                _sp.AddParameterValue("@DB", "GEO");
                                _sp.AddParameterValue("@ProcedureName", "pGeoSessionDetAdd");
                                _sp.AddParameterValue("@Parameters", r.ProcedureParameters());
                                _sp.AddParameterValue("@TableDB", "");
                                _sp.AddParameterValue("@TableName", "");
                                _sp.AddParameterValue("@TablePK", "");
                                try
                                {
                                    await _sp.ExecuteAsync();
                                    if (_sp.LastMsg.Substring(0, 2) != "OK")
                                    {
                                        Transmitting = false;
                                        return;
                                    }
                                    else
                                    {
                                        r.Transmitted = true;
                                        await Values.SQLidb.db.UpdateAsync(r);
                                        Values.sFt.UpdateInfo();
                                        //switch (r.Action)
                                        //{
                                        //    case "CHECK":
                                        //        Values.sFt.CheckQtyTransmitted++;
                                        //        break;
                                        //    case "ADD":
                                        //        Values.sFt.ReadQtyTransmitted++;
                                        //        break;
                                        //}
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Transmitting = false;
                                    Values.dFt.SetMessage(ex.Message);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Transmitting = false;
                            return;
                        }

                    }
                }
                catch
                {
                    Transmitting = false;
                    return;
                }
            }
            //Values.dFt.SetMessage("");
            Transmitting = false;
            return;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Monitor = new NetworkStatusMonitor(this);
            Monitor.NetworkStatusChanged += Monitor_NetworkStatusChanged;
            Monitor.Start();
            new Task(async () => await DoWork()).Start();
            return StartCommandResult.Sticky;
        }

    }
}