import {
  Box,
  CardContent,
  Typography,
  Button,
  Stack,
  IconButton,
  Divider,
} from '@mui/material';
import {
  Email as EmailIcon,
  Facebook as FacebookIcon,
  Twitter as TwitterIcon,
  LinkedIn as LinkedInIcon,
  Instagram as InstagramIcon,
  // Add other social icons you might need
} from '@mui/icons-material';


interface ContactUsPanelProps {
  email?: string; // Optional string
  emailSubject?: string; // Optional string
  socialLinks?: { name: string; url: string }[]; // Optional array of objects
}

// --- Define Prop Types for clarity (Optional, but recommended in TypeScript or with prop-types) ---
/**
 * @typedef {Object} SocialLink
 * @property {string} name - The name of the social platform (e.g., 'Twitter').
 * @property {React.ReactElement} icon - The Material UI Icon component.
 * @property {string} url - The full URL for the social profile.
 */

// A helper mapping to automatically select the icon based on the name (optional)
const socialIconMap: Record<string, React.ReactElement> = {
  facebook: <FacebookIcon />,
  twitter: <TwitterIcon />,
  linkedin: <LinkedInIcon />,
  instagram: <InstagramIcon />,
  // Extend this map for other platforms
};

/**
 * ContactPanelWithProps component displays an email address and a set of social links.
 * @param {Object} props
 * @param {string} props.email - The contact email address (e.g., 'support@company.com').
 * @param {string} [props.emailSubject='Inquiry from Website'] - The default subject for the mailto link.
 * @param {SocialLink[]} props.socialLinks - An array of social link objects.
 */
const ContactUsPanel = ({
  email = '',
  emailSubject = 'Inquiry from Website',
  socialLinks = [] // Default to an empty array if no links are provided
}: ContactUsPanelProps) => {
  // Function to handle the click and open the default mail client
  const handleEmailClick = () => {
    if (email) {
      window.location.href = `mailto:${email}?subject=${encodeURIComponent(emailSubject)}`;
    }
  };

  return (
    <Box justifyContent={'center'}>
      <CardContent sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center' }}>
        <EmailIcon color="primary" sx={{ fontSize: 48, mb: 1.5 }} />

        <Typography variant="h5" component="div" gutterBottom>
          Get in Touch
        </Typography>

        <Typography variant="body2" color="text.secondary" sx={{ mb: 3 }}>
          Reach out to discuss tailored sponsorship packages or if you have any questions. 
        </Typography>

        {/* --- Email Contact Block (Only render if email prop is provided) --- */}
        {email && (
          <>
            <Stack
              direction="column"
              alignItems="center"
              spacing={1}
              sx={{ p: 2, mb: 3, bgcolor: 'action.hover', borderRadius: 1 }}
            >
              <Typography variant="h6" sx={{ fontWeight: 'bold' }}>
                {email}
              </Typography>

            </Stack>
            <Button
              variant="contained"
              color="secondary"
              startIcon={<EmailIcon />}
              onClick={handleEmailClick}
            >
              Send Us an Email
            </Button>
          </>
        )}

        {/* --- Social Media Links (Only render if links are provided) --- */}
        {socialLinks.length > 0 && (
          <>
            <Divider sx={{ mb: 2 }} />
            <Typography variant="subtitle1" component="div" sx={{ mb: 1 }}>
              Connect with us:
            </Typography>
            <Stack direction="row" spacing={1} justifyContent="center">
              {socialLinks.map((social) => {
                // Prioritize icon provided in the social link object, otherwise try to use the map
                const iconToUse = socialIconMap[social.name.toLowerCase()];

                if (!iconToUse) {
                  console.warn(`No icon found for social platform: ${social.name}`);
                  return null;
                }

                return (
                  <IconButton
                    key={social.name}
                    aria-label={social.name}
                    color="secondary"
                    href={social.url}
                    target="_blank"
                    rel="noopener noreferrer"
                    size="large"
                  >
                    {iconToUse}
                  </IconButton>
                );
              })}
            </Stack>
          </>
        )}
      </CardContent>
    </Box>
  );
};

export default ContactUsPanel;