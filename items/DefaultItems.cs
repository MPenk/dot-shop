namespace dot_shop
{
    /// <summary>
    /// Klasa posiadająca przykładowe przedmioty.
    /// </summary>
    class DefaultItems
    {
        /// <summary>
        /// Dodawanie utowrzonych przez programistę przykładowych przedmiotów.
        /// </summary>
        public static void createDefaultItems()
        {

            ///////////////
            //CPU
            ///////////////
            new ItemCPU("Super procesor INTEL I3", "i3-9100F", "Intel", 3600, 4200, 6, 4, 4, 
                "https://cdn.x-kom.pl/i/setup/images/prod/big/product-large,,2019/5/pr_2019_5_21_8_56_30_228_01.jpg");
            new ItemCPU("Super procesor INTEL I5", "i5-4460", "Intel", 3200, 3400, 6, 4, 4, 
                "https://f.allegroimg.com/s360/03c352/f68679b34e5482f39f503956ad3f/NOWY-INTEL-I5-4460S-3-4GHz-LGA1150-COOLER-PASTA");
            new ItemCPU("Super procesor INTEL I9", "i9-10900K", "Intel", 3700, 5300, 20, 10, 20, 
                "https://images.morele.net/i1064/6730996_1_i1064.jpg");
            new ItemCPU("Procesor INTEL I7", "i7-8700K", "Intel", 3700, 4700, 12, 6, 12, 
                "https://images.morele.net/i1064/978219_0_i1064.jpg");
            new ItemCPU("Procesor Ryzen", "Ryzen 7 2700X", "AMD", 3700, 4300, 20, 8, 16, 
                "https://pcforce.pl/userdata/gfx/d2cb6fe0bb9d02903bac34d8a4ad212f.jpg");
            new ItemCPU("Procesor Ryzen Threadripper", "Ryzen Threadripper 1950X", "AMD", 3400, 4000, 32, 16, 32, 
                "https://8.allegroimg.com/s1024/0c1648/d375b0f04f9abec3e1f0ae884e48");


            ///////////////
            //GPU
            ///////////////
            new ItemGPU("Super grafika GeForce", "GTX 670", "Nvidia", 2, "GDDR5", 170, 
                "https://images.morele.net/i1064/517458_0_i1064.jpg");
            new ItemGPU("Ultra grafika GeForce", "RTX 3090", "Nvidia", 24, "GDDR6X", 350, 
                "https://thetechdudz.com/wp-content/uploads/2020/09/nvidia-rtx-3090.jpg");
            new ItemGPU("Karta graficzna Radeon", "Radeon RX5700 XT", "AMD", 8, "GDDR6", 225, 
                "https://images.morele.net/i1064/6346461_0_i1064.jpg");
            new ItemGPU("Karta graficzna Radeon", "Radeon RX6800","AMD",16, "GDDR6",250, 
                "https://www.purepc.pl/image/news/2020/11/16_amd_radeon_rx_6800_i_rx_6800xt_nowe_testy_wydajnosci_kart_0_b.png");

            ///////////////
            //RAM
            ///////////////
            new ItemRAM("Pamięć RAM Corsair Vengeance", "CMK16GX4M2B3000C15R", "Corsair",16,2,"DDR4", 3000 ,"CL15", 
                "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,pr_2015_9_7_15_35_38_218.jpg");
            new ItemRAM("Pamięć RAM HyperX Predator", " HX432C16PB3K2/16", "HyperX",16,2,"DDR4",3200,"CL16", 
                "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2019/8/pr_2019_8_13_15_34_56_482_00.jpg");


            ///////////////
            //ZASILACZE
            ///////////////
            new ItemPowerSupply("Zasilacz komputerowy SilentiumPC Supremo", "SPC169", "SilentiumPC",750, "Certyfikat 80 PLUS Gold", 
                "https://cdn.x-kom.pl/i/setup/images/prod/big/product-large,,2018/7/pr_2018_7_31_12_52_45_559_01.jpg");
            new ItemPowerSupply("Zasilacz komputerowy be quiet! Pure Power 11", "BN299", "be quiet!", 700, "Certyfikat 80 PLUS Gold", 
                "https://cdn.x-kom.pl/i/setup/images/prod/big/product-new-big,,2018/11/pr_2018_11_2_13_44_35_661_02.jpg");



            ///////////////
            //PŁYTY GŁÓWNE
            ///////////////
            new ItemMotherboard("Płyta główna dla AMD", "B450 TOMAHAWK MAX", "MSI", "AMD B450", "AM4", "DDR4", 
                "https://www.mediaexpert.pl/media/cache/gallery/product/3/755/362/422/31go5tar/images/19/1962063/Plyta-glowna-MSI-B450-Tomahawk-Max-bok-lewy.jpg");
            new ItemMotherboard("Płyta główna dla Intel", "PRIME Z390-P", "ASUS", "Intel Z390", "1151", "DDR4",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS3LqlKkfktwGFeuqA8il_zbzNKCaQnYci1SQ&usqp=CAU"); 
            new ItemMotherboard("Ultra płyta główna dla Intel", "TUF GAMING Z490-PLUS (WI-FI)", "ASUS", "Intel Z490", "1200", "DDR4",
                "https://image.ceneostatic.pl/data/products/93923309/i-asus-tuf-gaming-z490-plus-wi-fi-90mb1330-m0eay0.jpg");

            ///////////////
            //DYSKI
            ///////////////
            new ItemDrive("SUPER szybki dysk SSD", "MX500", "Crucial", "SSD TLC", "SATA III", 1000,
                "https://f01.esfr.pl/si_upload/ALA/1141381/disc-image2.jpg");
            new ItemDrive("ultra wolny dysk HDD", "ST1000LM048", "Seagate", "HDD", "SATA III", 4000,
                "https://a.allegroimg.com/s1024/0c254f/5e6d4418425faf4f9034523c1fac"); 
            new ItemDrive("Szybciutki dysk SSD", "XPG SX8200 Pro  ASX8200PNP-512GT-C", "ADATA", "SSD TLC", "M.2 PCIe NVMe 3.0 x4", 512,
                "https://proline.pl/pic/asx8200pnp-512gt-c_1.jpg");


            //////////////////////////////////////////////////////////


            //TODO: Dodać więcej przedmiotów do wyświetlenia
        }
    }
}
