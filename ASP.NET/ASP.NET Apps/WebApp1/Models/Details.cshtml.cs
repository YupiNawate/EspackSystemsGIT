﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;

namespace WebApp1.Models
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp1.Data.WebApp1Context _context;

        public DetailsModel(WebApp1.Data.WebApp1Context context)
        {
            _context = context;
        }

        public MasterFruta MasterFruta { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MasterFruta = await _context.MasterFruta.FirstOrDefaultAsync(m => m.Fruta_Id == id);

            if (MasterFruta == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}