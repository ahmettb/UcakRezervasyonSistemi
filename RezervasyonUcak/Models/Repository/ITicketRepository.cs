namespace RezervasyonUcak.Models.Repository
{
    public interface ITicketRepository
    {
        ICollection<Bilet> getAllTicket();

        ICollection<Bilet> getAllTicketAndDeletedFalse();
        Bilet getTicketByTicketNo(string ticketNo);
        ICollection<Bilet> getTicketsByUsername(string username);
        ICollection<Bilet> getTicketsByMail(string mail);

        void updateTicket(string ticketNo);
        void deleteTicket(string ticketNo);
        void addTicket(Bilet ticket );


    }
}
