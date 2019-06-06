			DrinkAndGo
			
	* Create the WebApi
		- File -> New -> Project -> .Net Core -> ASP.NET Core Web Application(.NET Core)
		- We are going to see tree dotnet core templates for web
			- Web application -> dotnet Core MVC application with example views and controllers.
			- Web API -> dotnet Core application for restful HTTP services.
			- Empty -> project with no content in it. (that we chose for this project)	
		
	* IsDevelopment() & IsProduction() (main working environments on an .NET Core in startup.cs)
	
	* Configuration of the application
		- Microsoft.AspNetCore.Razor.Tools
		- Microsoft.AspNetCore.StaticFiles
		- Microsoft.AspNetCore.Mvc
			- In Startup.cs -> ConfigureServives method
				- services.AddMvc();
			- In Startup.cs -> Configure method
				- loggerFactory.AddConsole();
				- app.UseDeveloperExceptionPage();
				- app.UseStatusCodes();
				- app.UseStaticFiles();
				- app.UseMvcWhithDefaultRoute();

	* What is MVC in .Net Core?
		- Model-View-Controller
		- Design pattern
		- clear separation of logic: Model, View and Controller.
		- Testable and Maintanable
		* Model 	 -> Contains the data the user works with (the model logic).
					 -> Classes will represent the models we are going to work with.
		* Controller -> Connecter the model with the View.
					 -> Theprovide the logi that works on the data model and the
						results are displayed on the view.
		* View 		 -> Will display the data to the user.
					 -> The Views are typically created from the model data.
	 
	* Adding models to the project:
		- In Data -> Models -> Drink.cs 
			+ DrinkId
			+ Name
			+ ShortDescription
			+ LongDescription
			+ Price
			+ ImageUrl
			+ ImageThumbnailUrl
			+ IsPreferredDrink
			+ InStock
			+ CategoryId
			+ Category <- an object Category
			
		- In Data -> Models -> Category.cs 
			+ CategoryId 
			+ CategoryName 
			+ Description 
			+ List<Drink> Drinks
			
	* Adding repositories interfaces
		- In Data -> interfaces -> IDrinkRepository.cs 
			+ IEnumerable<Drink> Drinks { get; }
			+ IEnumerable<Drink> PreferredDrinks { get; }
			+ Drink GetDrinkById(int drinkId);
	
	* Adding dummy Mockers
		- In Data -> interfaces -> MockDrinkRepository.cs 
		public class MockDrinkRepository : IDrinkRepository
		{
			private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();

			public IEnumerable<Drink> Drinks
			{
				get
				{
					return new List<Drink>
					{
						new Drink {
							Name = "Beer",
							Price = 7.95M, ShortDescription = "The most widely consumed alcohol",
							LongDescription = "Beer is the world's oldest[1][2][3] and most widely consumed[4] alcoholic drink; it is the third most popular drink overall, after water and tea.[5] The production of beer is called brewing, which involves the fermentation of starches, mainly derived from cereal grains—most commonly malted barley, although wheat, maize (corn), and rice are widely used.[6] Most beer is flavoured with hops, which add bitterness and act as a natural preservative, though other flavourings such as herbs or fruit may occasionally be included. The fermentation process causes a natural carbonation effect, although this is often removed during processing, and replaced with forced carbonation.[7] Some of humanity's earliest known writings refer to the production and distribution of beer: the Code of Hammurabi included laws regulating beer and beer parlours.",
							Category = _categoryRepository.Categories.First(),
							ImageUrl = "http://img.youtube.com/vi/3_NSrlbu458/0.jpg",
							InStock = true,
							IsPreferredDrink = true,
							ImageThumbnailUrl = "http://img.youtube.com/vi/3_NSrlbu458/0.jpg"
						}, 
						...
				    };
					
					public IEnumerable<Drink> PreferredDrinks { get; }

					public Drink GetDrinkById(int drinkId)
					{
						throw new NotImplementedException();
					}
				}
			}
		}
		
	* Configure services
		- In Startup.cs -> ConfigureServives method (services.AddTransient<Interfaces, Implamentation>();)
			services.AddTransient<IDrinkRepository, MockCategoryRepository>(); (perche voliamo tirare fuori i dati dall Mock)
            services.AddTransient<ICategoryRepository, MockCategoryRepository>();
	
	* Adding a controller
		- In Controller -> DrinkController.cs
		public class DrinkController : Controller
		{
			private readonly IDrinkRepository _drinkRepository;
			private readonly ICategoryRepository _categoryRepository;

			public DrinkController(IDrinkRepository drinkRepository, ICategoryRepository categoryRepository)
			{
				_drinkRepository = drinkRepository;
				_categoryRepository = categoryRepository;
			}
			
			public ViewResult List(string category)
			{
				string _category = category;
				IEnumerable<Drink> drinks;

				string currentCategory = string.Empty;
				if (string.IsNullOrEmpty(category))
				{
					drinks = _drinkRepository.Drinks.OrderBy(n => n.DrinkId);
					currentCategory = "All Drinks";
				}
				else
				{
					if (string.Equals("Alcoholic", _category, StringComparison.OrdinalIgnoreCase))
						drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Alcoholic")).OrderBy(p => p.Name);
					else
						drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Non-Alcoholic")).OrderBy(p => p.Name);

					currentCategory = _category;
				}

				var drinkListViewModel = new DrinkListViewModel
				{
					Drinks = drinks,
					CurrentCategory = currentCategory
				};

				return View(drinkListViewModel);
			}
			
	* Adding a view
		- In Views -> Drink -> List.cshtml
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			