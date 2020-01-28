import React, { useState, useEffect } from "react";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Form, FormGroup, Label, Input } from 'reactstrap';
import axios from "axios";

export default function MailAddressEdit(props) {
    const { showModal, editedMailAddress, toggleModal, mailGroup, onGroupEdit } = props;
    const adding = editedMailAddress === undefined;

    const [mailAddress, setMailAddress] = useState(editedMailAddress ? editedMailAddress : { name: undefined, lastName: undefined, address: undefined, groupId: mailGroup.id });

    const createMailAddress = () => {
        debugger;
        axios.post('api/MailGroups/' + mailGroup.id + '/MailAddresses', mailAddress)
            .then(response => {
                debugger;
                mailGroup.addresses.push(response.data)
                toggleModal();
            });
    }

    return (
        <Modal isOpen={showModal}>
            <ModalHeader toggle={toggleModal}>{adding ? 'Adding ' : 'Editing '}mail address</ModalHeader>
            <ModalBody>
                <Form>
                    <FormGroup>
                        <Label for="mailAddress">E-mail</Label>
                        <Input
                            type="email"
                            id="mailAddress"
                            defaultValue={mailAddress.address}
                            onChange={e => {
                                setMailAddress({ ...mailAddress, address: e.target.value });
                            }}
                        />
                        <Label for="mailAddressName">Name</Label>
                        <Input
                            type="text"
                            id="mailAddressName"
                            defaultValue={mailAddress.name}
                            onChange={e => {
                                setMailAddress({ ...mailAddress, name: e.target.value });
                            }}
                        />
                        <Label for="mailAddressLastName">Last name</Label>
                        <Input
                            type="text"
                            id="mailAddressLastName"
                            defaultValue={mailAddress.lastName}
                            onChange={e => {
                                setMailAddress({ ...mailAddress, lastName: e.target.value })
                            }} />
                    </FormGroup>
                </Form>
            </ModalBody>
            <ModalFooter>
                <Button color={'secondary'} onClick={toggleModal} >Cancel</Button>
                <Button
                    color={'success'}
                    onClick={createMailAddress}>Save</Button>
            </ModalFooter>
        </Modal>
    )
}