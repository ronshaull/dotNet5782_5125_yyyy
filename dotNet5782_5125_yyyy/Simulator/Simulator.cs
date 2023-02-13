using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    /*
     * We define the delegates in the name space and we define it as public so everybody 
     * (specificlly in simulator window) could create a delegate of this instance,
     * how ever we use event of the same kind so only += and -= interface is accessable from outside.
     */
    public delegate void SimulationCompleteHandler();
    public delegate void UpdateEventHandler(BO.Order? current, BO.Enums.Status oldstatus, BO.Enums.Status newstatus, DateTime starttime, DateTime finishtime);

    public static class Simulator
    {
        #region properties
        private static volatile bool shouldstop;
        static readonly BlApi.IBL bl = BlApi.Factory.Get();
        private static event SimulationCompleteHandler? SimulationComplete; //event handler for completion of simulation.
        private static event UpdateEventHandler? Update;
        #endregion
        public static void StartSimulation()
        {
            shouldstop = false; //should not stop!
            new Thread(() =>
            {
                while (!shouldstop)
                {
                    try
                    {
                        BO.Order? curr = bl.Order.OrderToHandle();
                        if (curr is not null)
                        {
                            int random = new Random().Next(3, 11);
                            DateTime UpdateStartTime = DateTime.Now; //time that the update is finish.
                            DateTime UpdateFinishTime = UpdateStartTime + new TimeSpan(random * 10000000);
                            if (curr.ShipDate == DateTime.MinValue)
                            {
                                if (Update == null)
                                {
                                    shouldstop = true;
                                    continue;
                                }
                                //Update function reports to PL layer that we got a new order to handle so we could change the UI.
                                Update?.Invoke(curr, BO.Enums.Status.Placed, BO.Enums.Status.Shipped, UpdateStartTime, UpdateFinishTime);
                                Thread.Sleep(random * 1000); // we wait the handling time span.
                                bl.Order.UpdateShipment(curr.ID);
                            }
                            else //order was shipped but yet to be deliverd.
                            {
                                Update?.Invoke(curr, BO.Enums.Status.Shipped, BO.Enums.Status.Deliverd, UpdateStartTime, UpdateFinishTime);
                                Thread.Sleep(random * 1000);
                                bl.Order.UpdateDelivery(curr.ID);
                            }
                            SimulationComplete?.Invoke();
                        }
                        Thread.Sleep(1000);
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }
                }
            }).Start();
        }
        /// <summary>
        /// stop simultaor safly.
        /// </summary>
        public static void StopSimulator() => shouldstop = true;
        public static void SimulatorCompleteReg(SimulationCompleteHandler func)
        {
            SimulationComplete += func;
        }
        public static void SimulatorCompleteDeletion(SimulationCompleteHandler func)
        {
            SimulationComplete -= func;
        }
        public static void UpdateReg(UpdateEventHandler update)
        {
            Update += update; ;
        }
        public static void UpdateDeletion(UpdateEventHandler update)
        {
            Update -= update; ;
        }
    }
}