using AutoMapper;
using ExamForms.Constants;
using ExamForms.Models.Accounts;
using ExamForms.Repository;
using ExamForms.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamForms.Manager.Accounts;

public class AdministrationManager
{
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper _mapper;
    private readonly ILogger<AdministrationManager> logger;
    private readonly UserRepository userRepository;

    public AdministrationManager(RoleManager<ApplicationRole> roleManager
        , UserManager<ApplicationUser> userManager
        , IMapper _mapper
        , ILogger<AdministrationManager> logger
        , UserRepository userRepository)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
        this._mapper = _mapper;
        this.logger = logger;
        this.userRepository = userRepository;
    }

    public async Task<List<ApplicationUserViewModel>> GetUsersAsync()
    {
        List<ApplicationUser> users = await userManager.Users.ToListAsync();
        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            user.RoleName = roles.FirstOrDefault();
        }
        return _mapper.Map<List<ApplicationUserViewModel>>(users);
    }
}
