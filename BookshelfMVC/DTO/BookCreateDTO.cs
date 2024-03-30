namespace BookshelfMVC.DTO
{

    public record BookCreateDTO(
string Title,
string Description,
string ISBN,
DateTime PublishDate,
int NumPages,
List<int> AuthorIds);


}
