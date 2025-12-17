export interface Club {
  name: string;
  products: Product[]; // Array of product names
  email: string; // Contact email for the club
  primaryColor: string; // Primary color in hex format
  secondaryColor: string; // Secondary color in hex format
  socialMedia: {
    [key: string]: string | undefined; // Additional social media links
  };
}

export interface Product {
  id: string; // Unique identifier for the product
  title: string; // Name of the product
  price: string; // Price as a string (e.g., "$20", "€15") if no price contact us for more detail will be displayed
  description: string; // Brief description of the product
  iconWord: string; // Keyword representing the icon for the product
  tags?: string[]; // Optional array of tags for the product
}
 
export const sampleClub: Club = 
  {
    name: "Sunset FC",
    products: [
      {
        id: "1",
        title: "Team Jersey",
        price: "$50",
        description: "Official team jersey with customizable name and number.",
        iconWord: "soccerball", // Updated to match icon key
        tags: ["apparel", "team"]
      }
    ],
    email: "contact@sunsetfc.com",
    primaryColor: "#348c34",
    secondaryColor: "#000000ff",
    socialMedia: {
      twitter: "https://twitter.com/sunsetfc",
      facebook: "https://facebook.com/sunsetfc"
    }
  };