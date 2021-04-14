using System.Collections;
using System.Collections.Generic;
using Web.Models.Shared;

namespace HotelReservationsManager.Models.Accounts
{
    public class AccountsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<AccountsViewModel> Accounts { get; set; }
    }
}