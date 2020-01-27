import React, { useState } from "react";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Form, FormGroup, Label, Input } from 'reactstrap';
import axios from "axios";
import PropTypes from 'prop-types';

export default function MailGroupEdit(props) {

    const { showModal, toggleModal, onGroupEdit, editedGroup } = props;
    const adding = editedGroup === undefined;
    const [mailGroup, setMailGroup] = useState(editedGroup ? editedGroup : { name: undefined, description: undefined });

    const createMailGroup = mailGroup => {
        axios.post('api/MailGroups', mailGroup)
            .then(response => {
                onGroupEdit(response.data);
                toggleModal();
            });
    }

    const editMailGroup = mailGroup => {
        axios.put('api/MailGroups/' + mailGroup.id, mailGroup)
            .then(() => {
                onGroupEdit(mailGroup);
                toggleModal();
            });
    }

    return (
        <Modal isOpen={showModal}>
            <ModalHeader toggle={toggleModal}>{adding ? 'Adding ' : 'Editing '}mail group</ModalHeader>
            <ModalBody>
                <Form>
                    <FormGroup>
                        <Label for="mailGroupName">Name</Label>
                        <Input
                            type="text"
                            id="mailGroupName"
                            defaultValue={mailGroup.name}
                            onChange={e => {
                                setMailGroup({ ...mailGroup, name: e.target.value });
                            }} />
                        <Label for="mailGroupDescription">Description</Label>
                        <Input
                            type="textarea"
                            id="mailGroupDescription"
                            defaultValue={mailGroup.description}
                            onChange={e => {
                                setMailGroup({ ...mailGroup, description: e.target.value });
                            }} />
                    </FormGroup>
                </Form>
            </ModalBody>
            <ModalFooter>
                <Button color={'secondary'} onClick={toggleModal}>Cancel</Button>
                <Button
                    color={'success'}
                    onClick={() => {
                        if (adding)
                            createMailGroup(mailGroup);
                        else
                            editMailGroup(mailGroup);
                    }}>
                    Save
                </Button>
            </ModalFooter>
        </Modal>
    )
}

MailGroupEdit.propTypes = {
    showModal: PropTypes.bool,
    toggleModal: PropTypes.func.isRequired,
    onGroupEdit: PropTypes.func.isRequired,
    editedGroup: PropTypes.object
}