namespace ExpenseApprovalSystem.Domain.Enums
{
    public enum ActionType
    {
        Created = 0,      // Oluşturma
        Updated = 1,      // Güncelleme
        Approved = 2,     // Onaylama
        Rejected = 3,     // Reddetme
        Cancelled = 4,    // İptal etme
            
        //Submitted = 5,    // Gönderme
        //Returned = 6      // Geri gönderme
    }
}
