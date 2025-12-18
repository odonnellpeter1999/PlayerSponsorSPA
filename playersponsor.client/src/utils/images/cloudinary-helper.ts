import { Cloudinary } from "@cloudinary/url-gen";
import { fill } from "@cloudinary/url-gen/actions/resize";
import { quality } from "@cloudinary/url-gen/actions/delivery";

export class CloudinaryHelper {
  private cld: Cloudinary;

  constructor(cloudName: string) {
    this.cld = new Cloudinary({
      cloud: { cloudName }
    });
  }

  /**
   * Returns a Cloudinary image instance so you can chain transformations.
   */
  getImage(publicId: string) {
    return this.cld.image(publicId);
  }

  /**
   * Shorthand for a commonly-used transformed URL.
   * Good for thumbnails, avatars, preview images, etc.
   */
  getTransformedImageUrl(publicId: string, width = 400, height = 300) {
    return this.cld
      .image(publicId)
      .resize(fill().width(width).height(height))
      .delivery(quality("auto"))
      .toURL();
  }

  /**
   * Return a raw URL without transformations.
   */
  getRawImageUrl(publicId: string) {
    return this.cld.image(publicId).toURL();
  }
}
