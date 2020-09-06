using Autofac;

namespace TrailHeadTestApp
{
    public class DIService
   {
      private static IContainer container;
      public static IContainer Container
      {
         get => container;
         set
         {
            container = value;
         }
      }

      public T Resolve< T >()
      {
         return Container.Resolve<T>();
      }
   }
}
