// See https://aka.ms/new-console-template for more information
string greeting = @"Welcome to Thrown for a Loop!
Your one-stop shop for used sporting equipment";


List<Product> products = new List<Product>() {
    new Product() {
        Name = "Football",
        Price = 15.25M,
        SoldOnDate = null,
        StockDate = new DateTime(2023, 09, 21),
        ManufactureYear = 2010,
        Condition = 3.7
    },
    new Product() {
        Name = "Hockey Stick",
        Price = 12.99M,
        SoldOnDate = null,
        StockDate = new DateTime(2023, 10, 07),
        ManufactureYear = 2012,
        Condition = 4.6
    },
    new Product() {
        Name = "Boomerang",
        Price = 9.95M,
        SoldOnDate = null,
        StockDate = new DateTime(2023, 12, 18),
        ManufactureYear = 2014,
        Condition = 3.2
    },
    new Product() {
        Name = "Frisbee",
        Price = 6.79M,
        SoldOnDate = null,
        StockDate = new DateTime(2024, 01, 09),
        ManufactureYear = 2016,
        Condition = 2.9
    },
    new Product() {
        Name = "Golf Putter",
        Price = 3.50M,
        SoldOnDate = new DateTime(2024, 04, 07),
        StockDate = new DateTime(2024, 03, 17),
        ManufactureYear = 2018,
        Condition = 1.5
    },
    new Product() {
        Name = "Corn Hole Set",
        Price = 59.99M,
        SoldOnDate = new DateTime(2024, 04, 08),
        StockDate = new DateTime(2024, 03, 28),
        ManufactureYear = 2020,
        Condition = 4.2
    }
};


Console.WriteLine(greeting);

string choice = null;
while (choice != "0") {
    Console.WriteLine(@"Choose an option:
        0. Exit
        1. View All Products
        2. View Product Details
        3. View Latest Products
        4. View Monthly Report
    ");
    choice = Console.ReadLine()!;
    if (choice == "0") {
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1") {
        ListProducts();
    }
    else if (choice == "2") {
        ViewProductDetails();
    }
    else if (choice == "3") {
        ViewLatestProducts();
    }
    else if (choice == "4") {
        MonthlySalesReport();
    }
}


void ViewProductDetails() {
    ListProducts();


    Product chosenProduct = null;
    while (chosenProduct == null) {
        Console.WriteLine("Please enter a product number!");
        try {
            int response = int.Parse(Console.ReadLine()!.Trim());
            chosenProduct = products[response - 1];
        }
        catch (FormatException) {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException) {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
            Console.WriteLine("Do better!");
        }
    }


    // TimeSpan timeInStock = DateTime.Now - chosenProduct.StockDate;
    Console.WriteLine(@$"You chose: {chosenProduct.Name}, which costs {chosenProduct.Price} dollars.
    It is {DateTime.Now.Year - chosenProduct.ManufactureYear} years old, with a quality rating of {chosenProduct.Condition}.
    It {(chosenProduct.SoldOnDate ==  null ? "is not available." : $"has been in stock for {chosenProduct.TimeInStock.Days} days.")}");
}


void ListProducts() {
    decimal totalValue = 0.0M;
    foreach (Product product in products) {
        if (product.SoldOnDate == null) {
            totalValue += product.Price;
        }
    }
    Console.WriteLine($"Total inventory value: ${totalValue}");
    Console.WriteLine("Products:");
    for (int i = 0; i < products.Count; i++) {
        Console.WriteLine($"{i + 1}. {products[i].Name}");
    }
}

void ViewLatestProducts() {
    // Create a new empty List to store the latest Products
    List<Product> latestProducts = new List<Product>();

    // Calculate a DateTime 90 days in the past
    DateTime threeMonthsAgo = DateTime.Now - TimeSpan.FromDays(90);

    // Loop through the products
    foreach (Product product in products) {
        // Add a product to latestProducts if it fits the criteria
        if (product.StockDate > threeMonthsAgo && product.SoldOnDate == null) {
            latestProducts.Add(product);
        }
    }

    // Print out the latest products to the console
    for (int i = 0; i < latestProducts.Count; i++) {
        Console.WriteLine($"{i + 1}. {latestProducts[i].Name}");
    }
}

void MonthlySalesReport() 
{
    int chosenMonth = 0;
    int chosenYear = 0;
    DateTime chosenDate = new DateTime();

    while (chosenMonth == 0)
    {
        try
        {
            Console.WriteLine("Month:");
            chosenMonth = int.Parse(Console.ReadLine()!.Trim());
            chosenDate = new DateTime(0001, chosenMonth, 1);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose a valid month!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Please input an integer!");
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
            Console.WriteLine("Something went wrong, please try again!");
        }
    }

    while (chosenYear == 0)
    {
        try
        {
            Console.WriteLine("Year:");
            chosenYear = int.Parse(Console.ReadLine()!.Trim());
            chosenDate = new DateTime(chosenYear, chosenMonth, 1);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose a valid Year!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Please input an integer!");
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
            Console.WriteLine("Something went wrong, please try again!");
        }
    }

    List<Product> foundProducts = products.Where(product => product.SoldOnDate?.Month == chosenMonth & product.SoldOnDate?.Year == chosenYear).ToList();

    decimal totalSum = foundProducts.Sum(product => product.Price);

    Console.WriteLine($"Total Revenue for {chosenDate.ToString("MMMM yyyy")}: ${totalSum}");
}