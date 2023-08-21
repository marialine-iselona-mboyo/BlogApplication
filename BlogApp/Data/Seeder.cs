using BlogApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing.Drawing2D;

namespace BlogApp.Data
{
    public class Seeder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _context;

        public Seeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _context = serviceProvider.GetService<ApplicationDbContext>();

            if( _context == null )
            {
                throw new Exception("Could not instantiate database");
            }
        }

        public async Task Seed()
        {
            this.AddPosts();
            this.AddCategories();
            this.AddUsers();
            this.AddUserRoles();
            await AssignRoles();
        }

        private async Task AssignRoles()
        {
            
            await AssignRolesToUser("Admin@gmail.com", "Admin");
;
            await AssignRolesToUser("User@gmail.com", "User");
        }

        private async Task AssignRolesToUser(string email, string role)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var user = await userManager.FindByEmailAsync(email);

                    if(user != null)
                    {
                        var result = await userManager.AddToRoleAsync(user, role);
                        if (result.Succeeded)
                        {
                            Console.WriteLine($"Assigned {role} role to user {email} successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to assign {role} role to user {email}:");
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine($"- {error.Description}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"User with email {email} not found.");
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e}");
                throw;
            }
        }

        private void AddUserRoles()
        {
            CreateRole("Admin");
            CreateRole("User");
        }
        private void CreateRole(string role)
        {
            if (_context.Roles.Any(x => x.Name == role))
            {
                return;
            }
            _context.Roles.Add(new IdentityRole
            {
                Name = role,
                NormalizedName = role.ToUpper()
            });
            _context.SaveChanges();
        }

        private void AddPosts()
        {
            var isSeeded = _context.Post.Any(p => p.Id > 0);

            if (isSeeded) return;

            List<Post> listOfProducts = new();
            Post FirstPost = new Post()
            {
                //Id = 0,
                CategoryId = 1,
                Title = "Spirit",
                Author = "Spirito",
                Body = " Donec est dolor, venenatis a lectus non, " +
                "volutpat euismod ante. Sed sollicitudin ullamcorper " +
                "justo, eget efficitur lacus euismod quis.",
                CreatedAt = new DateTime(1900, 01, 01),
            };
            Post SecondPost = new Post()
            {
                //Id = 0,
                CategoryId = 2,
                Title = "Spirit",
                Author = "Spirito",
                Body = " Donec est dolor, venenatis a lectus non, " +
                "volutpat euismod ante. Sed sollicitudin ullamcorper " +
                "justo, eget efficitur lacus euismod quis.",
                CreatedAt = new DateTime(1900, 01, 01),
            };
            Post ThirdPost = new Post()
            {
                //Id = 0,
                CategoryId = 3,
                Title = "Spirit",
                Author = "Spirito",
                Body = " Donec est dolor, venenatis a lectus non, " +
                "volutpat euismod ante. Sed sollicitudin ullamcorper " +
                "justo, eget efficitur lacus euismod quis.",
                CreatedAt = new DateTime(1900, 01, 01),
            };
            Post FourthPost = new Post()
            {
                //Id = 0,
                CategoryId = 4,
                Title = "Spirit",
                Author = "Spirito",
                Body = " Donec est dolor, venenatis a lectus non, " +
                "volutpat euismod ante. Sed sollicitudin ullamcorper " +
                "justo, eget efficitur lacus euismod quis.",
                CreatedAt = new DateTime(1900, 01, 01),
            };
            Post FifthPost = new Post()
            {
                //Id = 0,
                CategoryId = 5,
                Title = "Spirit",
                Author = "Spirito",
                Body = " Donec est dolor, venenatis a lectus non, " +
                "volutpat euismod ante. Sed sollicitudin ullamcorper " +
                "justo, eget efficitur lacus euismod quis.",
                CreatedAt = new DateTime(1900, 01, 01),
            };

            listOfProducts.Add(FirstPost);
            listOfProducts.Add(SecondPost);
            listOfProducts.Add(ThirdPost);
            listOfProducts.Add(FourthPost);
            listOfProducts.Add(FifthPost);

            _context.Post.AddRange(listOfProducts);
            _context.SaveChanges();
        }


        private void AddCategories()
        {
            var currentbdata = _context.Category.Where(b => b.Id > 0).ToList();
            var isSeeded = _context.Category.Any(b => b.Id > 0);
            if (isSeeded) return;

            List<Category> listOfCategories = new();

            Category firstCategory = new Category()
            {
                Id = 0,
                Name = "Love"
            };
            Category SecondCategory = new Category()
            {
                Id = 0,
                Name = "Horror"
            };
            Category ThirdCategory = new Category()
            {
                Id = 0,
                Name = "Kids"
            };
            Category FourthCategory = new Category()
            {
                Id = 0,
                Name = "Families"
            };
            Category FifthCategory = new Category()
            {
                Id = 0,
                Name = "Sad"
            };

            listOfCategories.Add(firstCategory);
            listOfCategories.Add(SecondCategory);
            listOfCategories.Add(ThirdCategory);
            listOfCategories.Add(FourthCategory);
            listOfCategories.Add(FifthCategory);

            _context.Category.AddRange(listOfCategories);
            _context.SaveChanges();
        }

        private void AddUsers()
        {
            var isSeeded = _context.Users.Any();
            if (isSeeded) return;

            string firstUserEmail = "User@gmail.com";
            string SecondUserEmail = "Admin@gmail.com";
            IdentityUser FirstUser = new IdentityUser()
            {
                //Id = "0",
                UserName = "User",
                Email = "User@gmail.com",
                NormalizedEmail = firstUserEmail.ToUpper(),
                EmailConfirmed = false,
                PasswordHash = "Abc@123",
            };

            IdentityUser SecondUser = new IdentityUser()
            {
                //Id = "0",
                UserName = "Admin",
                Email = "Admin@gmail.com",
                NormalizedEmail = SecondUserEmail.ToUpper(),
                EmailConfirmed = false,
                PasswordHash = "Abc@123",
            };

            CreateUser(FirstUser, "Abc@123");
            CreateUser(SecondUser, "Abc@123");

        }
        private void CreateUser(IdentityUser user, string psswrd)
        {
            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, psswrd);
                user.PasswordHash = hashed;
                var userStore = new UserStore<IdentityUser>(_context);
                var result = userStore.CreateAsync(user).Result;
            }
        }
    }
}
