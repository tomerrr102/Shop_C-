using BlApi;
using BO;
namespace Simulation;

public static class Simulator
{
    private static IBL bl = BlApi.Factory.GetBL();

    //static readonly BL_Func? bl = Factory.GetBL();

    public static readonly Random random = new Random(DateTime.Now.Millisecond);

    private volatile static bool run;

    private static Order? order;

    private static event Action? stopSimulator;

    private static event Action<Order, Status?, DateTime, int>? updateSimulator;

    private static event Action<Status?>? UpdateComplete;

    public static void StartSimulation()
    {
        new Thread(() =>
        {
            run = true;

            while (run)
            {
                int? id = 1234; //bl!.BO_Order.GetOrder().OrderUniqCode(1111);
                if (id != null)
                {
                    try
                    {
                        order = bl!.BO_Order.GetOrder((int)id);
                    }
                    catch (MissingInformationExeptions e)
                    {
                        throw e;
                    }

                    int treatTime = random.Next(3, 11);

                    updateSimulator?.Invoke(order, order.OrderStatus + 1, DateTime.Now, treatTime);

                    Thread.Sleep(treatTime * 1000);

                    if (order.OrderStatus == Status.Provided)
                    {
                        bl!.BO_Order.UpdateOrder(order.OrderUniqCode);
                        UpdateComplete?.Invoke(Status.Shiped);
                    }
                    else
                    {
                        bl!.BO_Order.OrderTrack(order.OrderUniqCode);
                        UpdateComplete?.Invoke(Status.Confirmed);
                    }
                }
                else
                    StopSimulation();

                Thread.Sleep(1000);
            }
        }).Start();
    }


    public static void StopSimulation()
    {
        stopSimulator?.Invoke();
        run = false;
    }


    public static void RegisterToStop(Action action) => stopSimulator += action;
    public static void DeRegisterToStop(Action action) => stopSimulator -= action;

    public static void RegisterToComplete(Action<Status?> action) => UpdateComplete += action;
    public static void DeRegisterToComplete(Action<Status?> action) => UpdateComplete -= action;

    public static void RegisterToUpdtes(Action<Order, Status?, DateTime, int> action) => updateSimulator += action;
    public static void DeRegisterToUpdtes(Action<Order, Status?, DateTime, int> action) => updateSimulator -= action;

}