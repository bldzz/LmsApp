﻿using Domain.Models.Entites;

namespace Domain.Contracts;

public interface IModuleRepo : IRepositoryBase<Module>
{
    // Fetch all modules with related data
    Task<IEnumerable<Module>> GetAllModulesAsync(bool trackChanges = false);

    // Fetch a module by ID with related data
    Task<Module?> GetModuleByIdAsync(int moduleId, bool trackChanges = false);

    // Fetch all modules for a specific course
    Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId, bool trackChanges = false);

    // Check if a module exists by ID
    Task<bool> ModuleExistsAsync(int moduleId);
}