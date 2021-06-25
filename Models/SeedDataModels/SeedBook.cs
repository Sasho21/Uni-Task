using LibraryAssistant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LibraryAssistant.Models.SeedDataModels
{
    public static class SeedBook
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LibraryContext>>()))
            {
                // Look for any movies.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                    new Book
                    {
                        Title = "Guards! Guards!",
                        PublishDate = DateTime.Parse("1989-2-12"),
                        Pages = 376,
                        isTaken = false,
                        Resume = "Long believed extinct, a superb specimen of draco nobilis (\"noble dragon\" for those who don't understand italics) has appeared in Discworld's greatest city. Not only does this unwelcome visitor have a nasty habit of charbroiling everything in its path, in rather short order it is crowned King (it is a noble dragon, after all...). How did it get there? How is the Unique and Supreme Lodge of the Elucidated Brethren of the Ebon Night involved? Can the Ankh-Morpork City Watch restore order – and the Patrician of Ankh-Morpork to power? Magic, mayhem, and a marauding dragon...who could ask for anything more?",
                        AuthorId = null,
                        CustomerId = null,
                        Deleted = false
                    },

                    new Book
                    {
                        Title = "The Metamorphosis",
                        PublishDate = DateTime.Parse("1915-3-13"),
                        Pages = 64,
                        isTaken = false,
                        Resume = "\"As Gregor Samsa awoke one morning from uneasy dreams he found himself transformed in his bed into a gigantic insect. He was laying on his hard, as it were armor-plated, back and when he lifted his head a little he could see his domelike brown belly divided into stiff arched segments on top of which the bed quilt could hardly keep in position and was about to slide off completely. His numerous legs, which were pitifully thin compared to the rest of his bulk, waved helplessly before his eyes.\" \n With it's startling, bizarre, yet surprisingly funny first opening, Kafka begins his masterpiece, The Metamorphosis. It is the story of a young man who, transformed overnight into a giant beetle-like insect, becomes an object of disgrace to his family, an outsider in his own home, a quintessentially alienated man. A harrowing—though absurdly comic—meditation on human feelings of inadequacy, guilt, and isolation, The Metamorphosis has taken its place as one of the most widely read and influential works of twentieth-century fiction. As W.H. Auden wrote, \"Kafka is important to us because his predicament is the predicament of modern man.\" ",
                        AuthorId = null,
                        CustomerId = null,
                        Deleted = false
                    },

                    new Book
                    {
                        Title = "So Long, and Thanks for All the Fish",
                        PublishDate = DateTime.Parse("1984-3-13"),
                        Pages = 192,
                        isTaken = false,
                        Resume = "Back on Earth with nothing more to show for his long, strange trip through time and space than a ratty towel and a plastic shopping bag, Arthur Dent is ready to believe that the past eight years were all just a figment of his stressed-out imagination. But a gift-wrapped fishbowl with a cryptic inscription, the mysterious disappearance of Earth’s dolphins, and the discovery of his battered copy of The Hitchhiker’s Guide to the Galaxy all conspire to give Arthur the sneaking suspicion that something otherworldly is indeed going on.\n God only knows what it all means. Fortunately, He left behind a Final Message of explanation. But since it’s light-years away from Earth, on a star surrounded by souvenir booths, finding out what it is will mean hitching a ride to the far reaches of space aboard a UFO with a giant robot. What else is new?",
                        AuthorId = null,
                        CustomerId = null,
                        Deleted = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}