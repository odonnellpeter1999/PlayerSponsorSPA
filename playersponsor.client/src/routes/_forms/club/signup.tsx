import { createFileRoute } from '@tanstack/react-router'
import { Box, Paper, Typography, TextField, Button, Alert, Divider, Container, Grid } from '@mui/material';
import { CreditCard, WarningAmber, Mail, Phone, CheckCircle } from '@mui/icons-material';
import { createUser } from '../../../queries/users/mutation-functions';
import { CreateClubRequest } from '../../../queries/users/types';
import { useForm } from '@tanstack/react-form';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { usersKeys } from '../../../queries/keys';
import { ApiErrorResponse } from '../../../api/client';
import { useState } from 'react';
import FieldInfo from '../../../components/forms/FieldInfo';

const SignUp = () => {
    
    // State
    const [validationErrors, setValidationErrors] = useState<Record<string, string[]>>({});
    const [serverError, setServerError] = useState<string>("");

    const form = useForm({
        onSubmit: async ({ value }) => onSubmit(value as CreateClubRequest),
    });

    const qc = useQueryClient();

    const createClubMutation = useMutation({
        mutationFn: createUser,
        onSuccess: () => qc.invalidateQueries({ queryKey: usersKeys.all }),
        onError: (errors) => handleErrors(errors as ApiErrorResponse)
    });

    // State Change Handlers
    const handleSave = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        await form.handleSubmit(e);
    };

    const handleErrors = (apiResponse: ApiErrorResponse) => {

        if (apiResponse.status == 400 && apiResponse.errors)
            setValidationErrors(apiResponse.errors || {});
            const toCamelCase = (str: string) => {
                return str
                    .split('.')
                    .map((word) =>
                        word.charAt(0).toLowerCase() + word.slice(1)
                    )
                    .join('.');
            };

            const camelCaseErrors: Record<string, string[]> = {};
            Object.keys(apiResponse.errors || {}).forEach((key) => {
                camelCaseErrors[toCamelCase(key)] = (apiResponse.errors ?? {})[key];
            });

            setValidationErrors(camelCaseErrors);
        if (apiResponse.title && apiResponse.status == 400)
            setServerError(apiResponse.title);

        return;
    };

    const onSubmit = async (request: CreateClubRequest) => {
        await createClubMutation.mutateAsync(request);
    };

    return (
        <Container maxWidth="md" sx={{ py: 4, minHeight: "100vh", backgroundColor: "#78a2cdff" }}>
            <Paper elevation={4} sx={{ borderRadius: '8px' }}>
                <Box sx={{ p: 2, display: 'flex', justifyContent: 'center', alignItems: 'center', bgcolor: 'transparent', borderTopLeftRadius: '8px', borderTopRightRadius: '8px' }}>
                    <Box
                        component="img"
                        src={'/playersponsor-logo.png'}
                        alt="PlayerSponsor logo"
                        sx={{ height: 80, objectFit: 'contain' }}
                    />
                </Box>
                <Divider />

                {/* Header (Simulating AppBar) */}
                <Grid container justifyContent={'center'} sx={{ p: 3, borderTopLeftRadius: '8px', borderTopRightRadius: '8px' }} >
                    <Typography variant="h4" component="h1" sx={{ fontWeight: 700, display: "flex", alignItems: "center", gap: 1 }}>
                        Create Your Club Account
                    </Typography>
                </Grid>

                {/* Status Messages (Simulating Alert) */}
                {createClubMutation.isSuccess && (
                    <Alert icon={<CheckCircle fontSize="inherit" />} severity="success" sx={{ borderRadius: 0 }}>
                        Details saved successfully and published!
                    </Alert>
                )}

                <Box component="form" onSubmit={handleSave} sx={{ p: { xs: 3, sm: 4 }, display: "flex", flexDirection: "column", gap: 5 }}>
                    {/* Section 1: Public Identity */}
                    <Box>
                        <Typography variant="h5" component="h2" sx={{ fontWeight: 600, mb: 3, pb: 1, borderBottom: "1px solid #eee" }}>
                            Public Club Details
                        </Typography>

                        <form.Field
                            name="clubDetails.name"
                            children={(field) => (
                                <>
                                    <TextField
                                        error={field.state.meta.isTouched && !field.state.meta.isValid}
                                        label="Club Name (Title)"
                                        fullWidth
                                        margin="normal"
                                        variant="outlined"
                                        value={field.state.value}
                                        onChange={(e) => field.handleChange(e.target.value)}
                                        required
                                    />
                                    <FieldInfo field={field} formValidationErrors={validationErrors} />
                                </>
                            )}
                        />

                        <form.Field
                            name="clubDetails.description"
                            children={(field) => (
                                <>
                                    <TextField
                                        label="Description (Public Text)"
                                        fullWidth
                                        margin="normal"
                                        variant="outlined"
                                        multiline
                                        rows={4}
                                        value={field.state.value}
                                        onChange={(e) => field.handleChange(e.target.value)}
                                        helperText="This text appears prominently on your public support page."
                                        required
                                    />
                                    <FieldInfo field={field} formValidationErrors={validationErrors} />
                                </>
                            )}
                        />
                    </Box>
                    <Divider />
                    {/* Section 2: Administrative Account Information */}
                    <Box>
                        <Typography variant="h5" component="h2" sx={{ fontWeight: 600, mb: 3, pb: 1, borderBottom: "1px solid #eee" }}>
                            Administrative Account Information
                        </Typography>

                        <form.Field
                            name="adminAccountDetails.email"
                            children={(field) => (
                                <>
                                    <TextField
                                        label="Public Contact Email"
                                        fullWidth
                                        margin="normal"
                                        variant="outlined"
                                        type="email"
                                        value={field.state.value}
                                        onChange={(e) => field.handleChange(e.target.value)}
                                        InputProps={{ startAdornment: <Mail sx={{ mr: 1, color: 'action.active' }} /> }}
                                        required
                                    />
                                    <FieldInfo field={field} formValidationErrors={validationErrors} />
                                </>
                            )}
                        />

                        <form.Field
                            name="adminAccountDetails.phoneNumber"
                            children={(field) => (
                                <>
                                    <TextField
                                        label="Admin Contact Phone (Optional)"
                                        fullWidth
                                        margin="normal"
                                        variant="outlined"
                                        type="tel"
                                        value={field.state.value}
                                        onChange={(e) => field.handleChange(e.target.value)}
                                        InputProps={{ startAdornment: <Phone sx={{ mr: 1, color: 'action.active' }} /> }}
                                    />
                                    <FieldInfo field={field} formValidationErrors={validationErrors} />
                                </>
                            )}
                        />


                        <form.Field
                            name="adminAccountDetails.password"
                            children={(field) => (
                                <>
                                    <TextField
                                        label="Password"
                                        fullWidth
                                        margin="normal"
                                        variant="outlined"
                                        type="password"
                                        value={field.state.value}
                                        helperText="Passwords should contain:
                                        uppercase character, 
                                        lowercase character, 
                                        digit, and a non-alphanumeric character, 
                                        and must also be at least six characters long."
                                        onChange={(e) => field.handleChange(e.target.value)}
                                    />
                                    <FieldInfo field={field} formValidationErrors={validationErrors} />
                                </>
                            )}
                        />

                        <form.Field
                            name="adminAccountDetails.confirmPassword"
                            validators={{
                                onChangeListenTo: ['adminAccountDetails.password'],
                                onChange: ({ value, fieldApi }) => {
                                    if (value !== fieldApi.form.getFieldValue('adminAccountDetails.password')) {
                                        return 'Passwords do not match'
                                    }
                                    return undefined
                                },
                            }}
                            children={(field) => (
                                <>
                                    <TextField
                                        label="Confirm Password"
                                        fullWidth
                                        margin="normal"
                                        variant="outlined"
                                        type="password"
                                        value={field.state.value}
                                        onChange={(e) => field.handleChange(e.target.value)}
                                    />
                                    <FieldInfo field={field} formValidationErrors={validationErrors} />
                                </>
                            )}
                        />
                    </Box>

                    <Divider />

                    {/* Section 3: Payment Configuration */}
                    <Box>
                        <Typography variant="h5" component="h2" color="error" sx={{ fontWeight: 600, mb: 3, pb: 1, borderBottom: "1px solid #eee", display: "flex", alignItems: "center", gap: 1 }}>
                            <CreditCard />
                            Interac e-Transfer Configuration
                        </Typography>

                        <Alert severity="warning" sx={{ mb: 2 }}>
                            <Typography variant="subtitle2" sx={{ fontWeight: 700 }}>CRITICAL PAYMENT SETTING:</Typography>
                            This is the exact email linked to your bank account for Interac Payments
                        </Alert>
                        <form.Field
                            name="clubDetails.interacEmail"
                            children={(field) => (
                                <>
                                    <TextField
                                        label="E-Transfer Recipient Email"
                                        fullWidth
                                        margin="normal"
                                        variant="outlined"
                                        type="email"
                                        value={field.state.value}
                                        onChange={(e) => field.handleChange(e.target.value)}
                                        sx={{ '& .MuiOutlinedInput-root': { borderColor: 'error.main' } }}
                                        required
                                    />
                                    <FieldInfo field={field} formValidationErrors={validationErrors} />
                                </>
                            )}
                        />
                    </Box>
                    {/* Global Error Message */}
                    {createClubMutation.isError && (
                        <Alert icon={<WarningAmber fontSize="inherit" />} severity="error" sx={{ borderRadius: 0 }}>
                            {serverError || 'An error occurred while creating the club. Please try again later.'}
                        </Alert>
                    )}
                    {/* Action Button */}
                    <Button
                        type="submit"
                        variant="contained"
                        color="primary"
                        size="large"
                        disabled={createClubMutation.isPending}
                        sx={{ mt: 2, py: 1.5, borderRadius: '24px', fontWeight: 600, fontSize: '1.1rem' }}
                    >
                        {createClubMutation.isPending ? 'Saving...' : 'Next'}
                    </Button>
                </Box>
            </Paper>
        </Container>
    );
};

export const Route = createFileRoute('/_forms/club/signup')({
    component: SignUp,
})