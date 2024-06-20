using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ProductSeed
    {
        public ModelBuilder Seed(ModelBuilder mdBuilder)
        {
            mdBuilder = SeedGenerico(mdBuilder);
            mdBuilder = SeedUsuario(mdBuilder);
            mdBuilder = SeedFaixa(mdBuilder);

            return mdBuilder;
        }

        public ModelBuilder SeedGenerico(ModelBuilder mdBuilder)
        {
            mdBuilder.Entity<Categoria>().HasData(
                new Categoria(1, "Brega Romântico", "/brega-romantico", "priscila-senna.jpeg"),
                new Categoria(2, "Brega Funk", "/brega-funk", "shevelloco.jpg"),
                new Categoria(3, "Brega das Antigas", "/brega-das-antigas", "reginaldo-rossi.jpg"),
                new Categoria(4, "Favoritas", "/favoritas", "biel-xcamoso.jpg")
                );
            return mdBuilder;
        }

        public ModelBuilder SeedUsuario(ModelBuilder mdBuilder)
        {
            mdBuilder.Entity<Usuario>().HasData(
                new Usuario("emmy", "00000000000", "Emmilly Lins", "31032003")
                );
            return mdBuilder;
        }

        public ModelBuilder SeedFaixa(ModelBuilder mdBuilder)
        {
            mdBuilder.Entity<Faixa>().HasData(
                new Faixa(1, "Tou Topando Tudo", "Metal & Cego", "<iframe style='border-radius:12px' src='https://open.spotify.com/embed/track/2aPvbQFgi8xYFi3pWhJMoI?utm_source=generator' width='100%' height='352' frameBorder='0' allowfullscreen='' allow='autoplay; clipboard-write; encrypted-media; fullscreen; picture-in-picture' loading='lazy'></iframe>", 2, "emmy"),
                new Faixa(2, "Melô do Amigo Safado", "Metal & Cego", "<iframe style='border-radius:12px' src='https://open.spotify.com/embed/track/2LoEQtvB0jbIVz2UR9Ej7G?utm_source=generator' width='100%' height='352' frameBorder='0' allowfullscreen='' allow='autoplay; clipboard-write; encrypted-media; fullscreen; picture-in-picture' loading='lazy'></iframe>", 2, "emmy"),
                new Faixa(3, "Vou Calar Sua Boca", "Banda Carícias", "<iframe style='border-radius:12px' src='https://open.spotify.com/embed/track/6hOt7PRPLGLw8Vc0cixp9H?utm_source=generator' width='100%' height='152' frameBorder='0' allowfullscreen='' allow='autoplay; clipboard-write; encrypted-media; fullscreen; picture-in-picture' loading='lazy'></iframe>", 1, "emmy"),
                new Faixa(4, "Curva Perigosa", "Priscila Senna", "<iframe style='border-radius:12px' src='https://open.spotify.com/embed/track/2NtMQUoZUUMqUvHjXnLhtp?utm_source=generator' width='100%' height='152' frameBorder='0' allowfullscreen='' allow='autoplay; clipboard-write; encrypted-media; fullscreen; picture-in-picture' loading='lazy'></iframe>", 1, "emmy"),
                new Faixa(5, "Mulher do Patrão", "MC Vertinho, MC Dinho", "<iframe style='border-radius:12px' src='https://open.spotify.com/embed/track/6WGtcsF850S6m6I29R38XW?utm_source=generator' width='100%' height='352' frameBorder='0' allowfullscreen='' allow='autoplay; clipboard-write; encrypted-media; fullscreen; picture-in-picture' loading='lazy'></iframe>", 2, "emmy")
                );
            return mdBuilder;
        }
    }
}
