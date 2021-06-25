using LibraryAssistant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LibraryAssistant.Models.SeedDataModels
{
    public static class SeedAuthor
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LibraryContext>>()))
            {
                // Look for any movies.
                if (context.Author.Any())
                {
                    return;   // DB has been seeded
                }

                context.Author.AddRange(
                    new Author
                    {
                        FirstName = "Terry",
                        LastName = "Pratchett",
                        BirthDate = DateTime.Parse("1948-4-24"),
                        Deleted = false,
                        About = "Sir Terence David John Pratchett OBE (28 April 1948 – 12 March 2015) was an English humorist, satirist, and author of fantasy novels, especially comical works. He is best known for his Discworld series of 41 novels. \n Pratchett, with more than 85 million books sold worldwide in 37 languages, was the UK's best-selling author of the 1990s. He was appointed Officer of the Order of the British Empire (OBE) in 1998 and was knighted for services to literature in the 2009 New Year Honours. In 2001 he won the annual Carnegie Medal for The Amazing Maurice and his Educated Rodents, the first Discworld book marketed for children. He received the World Fantasy Award for Life Achievement in 2010."
                    },

                    new Author
                    {
                        FirstName = "Franz",
                        LastName = "Kafka",
                        BirthDate = DateTime.Parse("1883-7-3"),
                        Deleted = false,
                        About = "Franz Kafka (3 July 1883 – 3 June 1924) was a German-speaking Bohemian novelist and short-story writer, widely regarded as one of the major figures of 20th-century literature. His work fuses elements of realism and the fantastic. It typically features isolated protagonists facing bizarre or surrealistic predicaments and incomprehensible socio-bureaucratic powers. It has been interpreted as exploring themes of alienation, existential anxiety, guilt, and absurdity. His best known works include \"Die Verwandlung\" (\"The Metamorphosis\"), Der Process (The Trial), and Das Schloss (The Castle). The term Kafkaesque has entered the English language to describe situations like those found in his writing."
                    },

                    new Author
                    {
                        FirstName = "Douglas",
                        LastName = "Adams",
                        BirthDate = DateTime.Parse("1952-3-11"),
                        Deleted = false,
                        About = "Douglas Noel Adams (11 March 1952 – 11 May 2001) was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was author of The Hitchhiker's Guide to the Galaxy, which originated in 1978 as a BBC radio comedy, before developing into a \"trilogy\" of five books that sold more than 15 million copies in his lifetime and generated a television series, several stage plays, comics, a video game, and in 2005 a feature film. Adams's contribution to UK radio is commemorated in The Radio Academy's Hall of Fame."
                    }
                );
                context.SaveChanges();
            }
        }
    }
}