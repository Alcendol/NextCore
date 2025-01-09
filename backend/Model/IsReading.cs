public class IsReading{
    public string? IsReadingId {get; set;}
    public string? userId {get; set;}
    public string? eBookId {get; set;}
    public string? lastPage {get; set;} // jika statusnya notset, lastpage bisa kosong
    public string? lastRead {get; set;} // Terakhir kali baca kapan (dihitung ketika status sudah ter-set)
    public string? status {get; set;} // bisa not set, not finished, finisihed, on progress (sifatnya optional)
}