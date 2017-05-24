namespace SmartShipment.UI.Common
{
    public abstract class BasePresenter<TView> : IPresenter where TView : IView
    {       
        protected TView View { get; }
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(IApplicationController controller, TView view)
        {            
            Controller = controller;
            View = view;
        }
        
        public void Run()
        {
            View.Show();
        } 
    }
}