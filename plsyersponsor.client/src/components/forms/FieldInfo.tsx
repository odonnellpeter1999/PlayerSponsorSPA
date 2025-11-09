import { Typography } from "@mui/material";
import { AnyFieldApi } from "@tanstack/react-form";

function FieldInfo({ field, formValidationErrors: formValidationErrors }: { field: AnyFieldApi, formValidationErrors: Record<string, string[]> }) {

    var errors = field.state.meta.errors.join(', ')

    if (formValidationErrors[field.name]) {
        field.state.meta.isValid = false;
        errors += (errors ? ', ' : '') + formValidationErrors[field.name].join(', ');
    }

    return (
        <>
            {errors ? (
                <Typography color="error">{errors}</Typography>
            ) : null}
            {field.state.meta.isValidating ? 'Validating...' : null}
        </>
    )
}

export default FieldInfo;